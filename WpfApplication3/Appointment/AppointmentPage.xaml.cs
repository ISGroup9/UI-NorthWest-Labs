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
    /// Interaction logic for AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        //set up the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //initialize the components, load up the datagrid
        public AppointmentPage()
        {
            InitializeComponent();
            InitializeData();
        }

        //get the table from the database and load it up into the datagrid
        public void InitializeData()
        {
            var sb = new StringBuilder();
            sb.Append("Select a.appointmentID as ApptID, s.clientID, client.FirstName + ' ' + client.Lastname AS ClientName,  a.StartTime AS AppDate, counselor.FirstName + ' ' + counselor.LastName AS CounselorName, a.RoomNumber, r.RoomName,employee.FirstName + ' ' + employee.LastName AS EmployeeName");
            sb.Append(" FROM Appointment a");
            sb.Append(" LEFT JOIN ScheduledFor s on s.AppointmentID = a.AppointmentID");
            sb.Append(" LEFT JOIN Person client ON client.personID = s.ClientID");
            sb.Append(" LEFT JOIN Person counselor ON counselor.personID = a.CounselorID");
            sb.Append(" LEFT JOIN Person employee ON employee.personID = a.EmployeeID");
            sb.Append(" JOIN Room r on a.roomNumber = r.roomNumber");
            sb.Append(" order by a.StartTime asc");

            string sql = sb.ToString();

            var appointments = _context.Database.SqlQuery<EmpItem>(sql).ToList();
            dgAppointments.ItemsSource = appointments;

            int index = 0;
            dgAppointments.SelectedItem = dgAppointments.Items[index];
            dgAppointments.ScrollIntoView(dgAppointments.Items[index]);
        }

        //when the update button is pressed, open a new window to update it
        private void btnUpdateAppointment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mySelectedRow = (EmpItem)dgAppointments.SelectedItem;
            var ClientID = mySelectedRow.ClientID.ToString();
            var ApptID = mySelectedRow.ApptID.ToString();
            var newDeleteClientWindow = new UpdateAppointment(ClientID, ApptID);
            newDeleteClientWindow.ShowDialog();
        }

        //when the new button is pressed, open a new window to create it
        private void btnNewAppointment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var NewAppointment = new NewAppointment();
            NewAppointment.ShowDialog();
        }

        //whent he delete button is pressed, open a new window to delete it
        private void btnDeleteAppointment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mySelectedRow = (EmpItem)dgAppointments.SelectedItem;
            var ClientID = mySelectedRow.ClientID.ToString();
            var ApptID = mySelectedRow.ApptID.ToString();
            var newDeleteClientWindow = new DeleteAppointment(ClientID, ApptID);
            newDeleteClientWindow.ShowDialog();
        }

        private void btnViewClientAppointment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mySelectedRow = (EmpItem)dgAppointments.SelectedItem;
            var ClientID = mySelectedRow.ClientID.ToString();
            var ClientName = mySelectedRow.ClientName.ToString();
            var newDeleteClientWindow = new ViewAppointment(ClientID, ClientName);
            newDeleteClientWindow.ShowDialog();
        }

        private void MaintainClient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //open the maintain clients page
            (((MainWindow)Application.Current.MainWindow).frame.Content) = new ClientPage();
        }



        //getters and setters for class
        public class EmpItem
        {
            public EmpItem() { }
            public int ApptID { get; set; }
            public int ClientID { get; set; }
            public string ClientName { get; set; }
            public DateTime? AppDate { get; set; }
            public string CounselorName { get; set; }
            public int RoomNumber { get; set; }
            public string RoomName { get; set; }
            public string EmployeeName { get; set; }
        }

        //update the datagrid when the mouse enters the datagrid
        private void dgAppointments_MouseEnter(object sender, MouseEventArgs e)
        {
            InitializeData();
        }
    }
}
