using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        //set up the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //initialize the components, load up the datagrid
        public ClientPage()
        {
            InitializeComponent();
            InitializeData();
        }

        //get the table from the database and load it up into the datagrid
        public void InitializeData()
        {
            var emplist = _context.Client.Select(e => new EmpItem() { ClientID = e.ClientID, First = e.Person.FirstName, Last = e.Person.LastName, City = e.Person.City, State = e.Person.State, ZIP = e.Person.ZIP, Phone = e.Person.Phone, Email = e.Person.Email, RemindMethod = e.ReminderMethod, FoundOut = e.FoundOut, ReferredBy = e.ReferredByClientID}).ToList();
            dgClients.ItemsSource = emplist;

            int index = 0;
            dgClients.SelectedItem = dgClients.Items[index];
            dgClients.ScrollIntoView(dgClients.Items[index]);
        }

        //when the update button is pressed, open a new window to update it
        private void btnUpdateClient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ClientTemp = (EmpItem)dgClients.SelectedItem;
            var Client = _context.Client.Where(em => em.ClientID == ClientTemp.ClientID).FirstOrDefault<Client>();
            var newEditClientWindow = new UpdateClient(Client);
            newEditClientWindow.ShowDialog();
        }

        //when the new button is pressed, open a new window to create it
        private void btnNewClient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ClientTemp = (EmpItem)dgClients.SelectedItem;
            var Client = _context.Client.Where(em => em.ClientID == ClientTemp.ClientID).FirstOrDefault<Client>();
            var newEditClientWindow = new NewClient(Client);
            newEditClientWindow.ShowDialog();
        }

        //whent he delete button is pressed, open a new window to delete it
        private void btnDeleteClient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ClientTemp = (EmpItem)dgClients.SelectedItem;
            var Client = _context.Client.Where(em => em.ClientID == ClientTemp.ClientID).FirstOrDefault<Client>();
            var newDeleteClientWindow = new DeleteClient(Client);
            newDeleteClientWindow.ShowDialog();
        }

        private void btnViewClientAppointment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mySelectedRow = (EmpItem)dgClients.SelectedItem;
            var ClientID = mySelectedRow.ClientID.ToString();

            //get the number of Clients in the appointment table
            //YOU HAVE TO FIGURE OUT HOW TO GET THE CLIENTID IN THE APPOINTMENTS TABLE
            var sb = new StringBuilder();
            sb.Append(" with apptstemp as");
            sb.Append("(Select a.appointmentID as ApptID, s.clientID, client.FirstName + ' ' + client.Lastname AS ClientName,  a.StartTime AS AppDate, counselor.FirstName + ' ' + counselor.LastName AS CounselorName, a.RoomNumber, r.RoomName,employee.FirstName + ' ' + employee.LastName AS EmployeeName");
            sb.Append(" FROM Appointment a");
            sb.Append(" LEFT JOIN ScheduledFor s on s.AppointmentID = a.AppointmentID");
            sb.Append(" LEFT JOIN Person client ON client.personID = s.ClientID");
            sb.Append(" LEFT JOIN Person counselor ON counselor.personID = a.CounselorID");
            sb.Append(" LEFT JOIN Person employee ON employee.personID = a.EmployeeID");
            sb.Append(" JOIN Room r on a.roomNumber = r.roomNumber)");
            sb.Append("SELECT count(*) FROM apptstemp where ClientID = " + ClientID);

            var sqlCnt = Convert.ToString(sb);
            var queryResult = _context.Database.SqlQuery<int>(sqlCnt).Single();

            //if it is greater than zero, the client will have appointments, so display them
            if (queryResult > 0)
            {
                var ClientName = mySelectedRow.First.ToString();
                ClientName = ClientName + ' ' + mySelectedRow.Last.ToString();
                var newDeleteClientWindow = new ViewAppointment(ClientID, ClientName);
                newDeleteClientWindow.ShowDialog();
            }
            //there are no appointments for the client
            else
            {
                MessageBox.Show("There are no appointments for this Client.");
            }
            
        }

        //getters and setters for class
        public class EmpItem
        {
            public EmpItem() { }
            public int ClientID { get; set; }
            public string First { get; set; }
            public string Last { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZIP { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string RemindMethod { get; set; }
            public string FoundOut { get; set; }
            public int? ReferredBy { get; set; }
        }



    }
}
