using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Data.Entity.Validation;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for UpdateAppointment.xaml
    /// </summary>
    public partial class UpdateAppointment : Window
    {
        //create the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //load up the Appointment, initialize components, populate fields from page to table
        public UpdateAppointment(string ClientID, string ApptID)
        {
            InitializeComponent();

            //load up the Counselor combobox
            var counselorlist = from c in _context.Counselor orderby c.Person.LastName select new { c.CounselorID, c.Person.LastName, Name = c.Person.LastName + ", " + c.Person.FirstName };
            cbCounselor.ItemsSource = counselorlist.ToList();
            cbCounselor.DisplayMemberPath = "Name";
            cbCounselor.SelectedValuePath = "CounselorID";

            //load up the Client combobox
            var Clientlist = from c in _context.Client orderby c.Person.LastName select new { c.ClientID, c.Person.LastName, Name = c.Person.LastName + ", " + c.Person.FirstName };
            cbClient.ItemsSource = Clientlist.ToList();
            cbClient.DisplayMemberPath = "Name";
            cbClient.SelectedValuePath = "ClientID";

            //load up the Employee combobox
            var Employeelist = from c in _context.Employee orderby c.Person.LastName select new { c.EmployeeID, c.Person.LastName, Name = c.Person.LastName + ", " + c.Person.FirstName };
            cbEmployee.ItemsSource = Employeelist.ToList();
            cbEmployee.DisplayMemberPath = "Name";
            cbEmployee.SelectedValuePath = "EmployeeID";

            //load up the Room combobox
            var Roomlist = from c in _context.Room orderby c.RoomName select new { c.RoomNumber, c.RoomName };
            cbRoom.ItemsSource = Roomlist.ToList();
            cbRoom.DisplayMemberPath = "RoomName";
            cbRoom.SelectedValuePath = "RoomNumber";


            //get the data for the fields
            var sb = new StringBuilder();
            sb.Append("with apptstemp as");
            sb.Append(" (Select a.appointmentID as ApptID, s.ClientID, a.StartTime AS AppDate, a.Duration, a.RoomNumber as RoomID, a.counselorid AS CounselorID, a.employeeID AS EmployeeID");
            sb.Append(" FROM Appointment a");
            sb.Append(" LEFT JOIN ScheduledFor s on s.AppointmentID = a.AppointmentID");
            sb.Append(" LEFT JOIN Person client ON client.personID = s.ClientID");
            sb.Append(" LEFT JOIN Person counselor ON counselor.personID = a.CounselorID");
            sb.Append(" LEFT JOIN Person employee ON employee.personID = a.EmployeeID");
            sb.Append(" JOIN Room r on a.roomNumber = r.roomNumber)");
            sb.Append(" SELECT * FROM apptstemp where ClientID = " + ClientID + " and ApptID = " + ApptID);

            string sql = sb.ToString();

            //create a datagrid
            DataGrid tempgrid = new DataGrid();

            //run the query and save it to the new datagrid
            var appointments = _context.Database.SqlQuery<UpdateAppt>(sql).ToList();
            tempgrid.ItemsSource = appointments;

            //select the only row in the data grid and extract the data
            tempgrid.SelectedIndex = 0;
            var mySelectedRow = (UpdateAppt)tempgrid.SelectedItem;

            //set up the new variables
            //split the ApptDate into date and time
            var ApptDate = mySelectedRow.AppDate.ToString();
                //split the strings
                string AppointTime = ApptDate.Substring(ApptDate.Length - 11);
                AppointTime = AppointTime.Trim();
                string AppointDate = ApptDate.Substring(0, (ApptDate.Length - 11));    

            var Duration = mySelectedRow.Duration.ToString();
            var RoomID = mySelectedRow.RoomID.ToString();
            var CounselorID = mySelectedRow.CounselorID.ToString();
            var EmployeeID = mySelectedRow.EmployeeID.ToString();

            //set the variables in the class
            txtbox_ApptID_Copy.Text = ApptID;
            dpDate_Copy.Text = AppointDate;
            cbTime_Copy.Text = AppointTime;
            cbDuration_Copy.Text = Duration;
            cbClient_Copy.SelectedValue = Convert.ToString(ClientID);
            cbCounselor_Copy.SelectedValue = Convert.ToString(CounselorID);
            cbEmployee_Copy.SelectedValue = Convert.ToString(EmployeeID);
            cbRoom_Copy.SelectedValue = Convert.ToString(RoomID);

            //load up the fields
            dpDate.Text = AppointDate;
            cbTime.Text = AppointTime;
            cbDuration.Text = Duration;
            cbClient.SelectedValue = Convert.ToString(ClientID);
            cbCounselor.SelectedValue = Convert.ToString(CounselorID);
            cbEmployee.SelectedValue = Convert.ToString(EmployeeID);
            cbRoom.SelectedValue = Convert.ToString(RoomID);
        }

        //when the update button is pressed, update the database
        public void btnUpdateAppointment_MouseDown(object sender, RoutedEventArgs e)
        {
                _context.Database.ExecuteSqlCommand("update appointment set starttime = @starttime, duration = @duration, counselorid = @counselorid, employeeid = @employeeid, roomnumber = @roomnumber where " +
                                                "AppointmentID = '" + txtbox_ApptID_Copy.Text  + "';",
                new SqlParameter("starttime", Convert.ToDateTime(dpDate.Text + ' ' + cbTime.Text)),
                new SqlParameter("duration", Convert.ToInt16(cbDuration.Text)),
                new SqlParameter("counselorid", Convert.ToInt16(cbCounselor.SelectedValue)),
                new SqlParameter("employeeid", Convert.ToInt16(cbEmployee.SelectedValue)),
                new SqlParameter("roomnumber", Convert.ToInt16(cbRoom.SelectedValue)));


            _context.Database.ExecuteSqlCommand("update scheduledfor set clientid = @ClientID where ClientID = " + cbClient_Copy.SelectedValue +
                                                " and AppointmentID = " + txtbox_ApptID_Copy.Text  + ";",
                new SqlParameter("ClientID", Convert.ToInt16(cbClient.SelectedValue)));

            ((AppointmentPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

            this.Close();

            MessageBox.Show("Appointment was successfully updated.");
        }

        public class UpdateAppt
        {
            public UpdateAppt() { }
            public int ApptID { get; set; }
            public int ClientID { get; set; }
            public DateTime? AppDate { get; set; }
            public int Duration { get; set; }
            public int RoomID { get; set; }
            public int CounselorID { get; set; }
            public int EmployeeID { get; set; }
        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}
