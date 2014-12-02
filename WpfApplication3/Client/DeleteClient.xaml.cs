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
using System.Windows.Shapes;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for DeleteClient.xaml
    /// </summary>
    public partial class DeleteClient : Window
    {
        //create the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //create get and set
        public Client _Client { get; set; }

        //initialize componenets, populate field from page to new window.
        public DeleteClient(Client Client)
        {
            _Client = Client;
            InitializeComponent();
            populateFields();
        }

        //populate the fields from the page to the window
        private void populateFields()
        {
            txtBoxClientID.Text = Convert.ToString(_Client.Person.Client.ClientID);
            txtBoxFName.Text = _Client.Person.FirstName;
            txtBoxLName.Text = _Client.Person.LastName;
            txtBoxPhone.Text = _Client.Person.Phone;
        }

        //when the delete button is pressed, delete
        private void btnDeleteClient_MouseDown(object sender, MouseButtonEventArgs e)
        {

            //get the number of Clients in the appointment table
            var sb = new StringBuilder();
            sb.Append(" with apptstemp as");
            sb.Append("(Select a.appointmentID as ApptID, s.clientID, client.FirstName + ' ' + client.Lastname AS ClientName,  a.StartTime AS AppDate, counselor.FirstName + ' ' + counselor.LastName AS CounselorName, a.RoomNumber, r.RoomName,employee.FirstName + ' ' + employee.LastName AS EmployeeName");
            sb.Append(" FROM Appointment a");
            sb.Append(" LEFT JOIN ScheduledFor s on s.AppointmentID = a.AppointmentID");
            sb.Append(" LEFT JOIN Person client ON client.personID = s.ClientID");
            sb.Append(" LEFT JOIN Person counselor ON counselor.personID = a.CounselorID");
            sb.Append(" LEFT JOIN Person employee ON employee.personID = a.EmployeeID");
            sb.Append(" JOIN Room r on a.roomNumber = r.roomNumber)");
            sb.Append("SELECT count(*) FROM apptstemp where ClientID = " + txtBoxClientID.Text);

            var sqlCnt = Convert.ToString(sb);
            var queryResult = _context.Database.SqlQuery<int>(sqlCnt).Single();

            if (queryResult > 0)
            {
                //display message to the user
                MessageBox.Show("Warning:  Due to HIPPA regulations, this Client cannot be deleted.");
            }
            else
            {
                //#1 - delete Client from the Client table 
                _context.Database.ExecuteSqlCommand("Delete from Client where ClientID =  " + txtBoxClientID.Text);

                //#2 - delete Client from the person table 
                _context.Database.ExecuteSqlCommand("Delete from Person where PersonID =  " + txtBoxClientID.Text);

                //reload the window to update the grid
                ((ClientPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

                //close the window
                this.Close();

                MessageBox.Show("Client was successfully deleted.");
            }
        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
