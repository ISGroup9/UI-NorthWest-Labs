using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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


namespace WpfApplication3
{
     //<summary>
     //Interaction logic for NewAppointment.xaml
     //</summary>
    public partial class NewAppointment : Window
    {
        //set up the database connection
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //set up get and set 
        public Appointment _Appointment { get; set; }

        //load up the components
        public NewAppointment()
        {
            //_Appointment = Appointment;
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

        }



        //when the create button is pressed, insert data from window into database
        private void btnCreateAppointment_MouseDown(object sender, RoutedEventArgs e)
        {
            _context.Database.ExecuteSqlCommand("insert into appointment(starttime, duration, counselorid, employeeid, roomnumber) values (@starttime, @duration, @counselorid, @employeeid, @roomnumber);",
                new SqlParameter("starttime", Convert.ToDateTime(dpDate.Text + ' ' + cbTime.Text)),
                new SqlParameter("duration", Convert.ToInt16(cbDuration.Text)),
                new SqlParameter("counselorid", Convert.ToInt16(cbCounselor.SelectedValue)),
                new SqlParameter("employeeid", Convert.ToInt16(cbEmployee.SelectedValue)),
                new SqlParameter("roomnumber", Convert.ToInt16(cbRoom.SelectedValue)));


            _context.Database.ExecuteSqlCommand("insert into scheduledfor values((select max(appointmentid) from appointment), @ClientID, null, null);",
                new SqlParameter("ClientID", Convert.ToInt16(cbClient.SelectedValue)));

            ((AppointmentPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

            this.Close();

            MessageBox.Show("Appointment was successfully added.");
        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}
