/// This program creates, updates, and deletes Counselors, Employees, Clients, Appointments, and Rooms 
/// in the database for the company NDFC.
/// Users can view appointments for specific clients/counselors. They can also view appoinments by date. 
/// 
/// Created By Landon Hulet
/// Updated Oct 18, 2014 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;





namespace NDFC
{
    public enum PersonType { COUNSELOR, EMPLOYEE, CLIENT, APPOINTMENT, ROOM };


    /// <summary>
    /// Interaction logic for MainWindow.xaml

    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        Entities db = new Entities(); 

        private DateTime? date;

        public MainWindow() // creates the window
        {
            InitializeComponent();
            date = Cal.DisplayDate.Date;
            AppointmentDateTxt.Text = "Appointments for " + date.ToString(); //Creates the header
            InitializeMyStuff();
        }

        private void InitializeMyStuff() //initializes the datagrid with the appointments for the day
        {

            //enables and disables the delete button if an item is selected and if the day is in the future
            if (date <= DateTime.Today || AppointmentDG.Items.Count == 0)
            {
                DeleteButton.IsEnabled = false;
                DeleteButton.ToolTip = "Due to HIPPA regulations, appointments in the past cannot be deleted";
            }
            else
            {
                DeleteButton.IsEnabled = true;
                DeleteButton.ToolTip = "Delete the selected appointment";
            }

            //updates the datagrid
            var appointments1 = from a in db.ScheduledFors
                                orderby a.Appointment.StartTime
                                where DbFunctions.TruncateTime(a.Appointment.StartTime) == date
                                select new
                                {
                                    ApID = a.AppointmentID,
                                    Time = a.Appointment.StartTime,
                                    Duration = a.Appointment.Duration,
                                    Room = a.Appointment.Room.RoomName,
                                    Client = a.Client.Person.FirstName + " " + a.Client.Person.LastName,
                                    Counselor = a.Appointment.Counselor.Person.FirstName + " " + a.Appointment.Counselor.Person.LastName,
                                    Employee = a.Appointment.Employee.Person.FirstName + " " + a.Appointment.Employee.Person.LastName,
                                    Notes = a.Appointment.Notes,
                                    Kept = a.kept
                                };

           


            
         

           
            try {
                AppointmentDG.ItemsSource = appointments1.ToList();
                AppointmentDG.SelectedValuePath = "ApID";
                
                
                AppointmentDG.SelectedIndex = 0;
                
                if (AppointmentDG.Items.Count == 0)
                {
                    editBtn.IsEnabled = false;
                }
                else
                {
                    editBtn.IsEnabled = true;
                    
                }
            }
            catch (System.Data.Entity.Core.EntityException) {
                MessageBox.Show("1");
                MessageBox.Show("Unable to Connect. Please check your connection settings");
                this.Close();
            }
            

        }
    
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) //exits the program
        {
            Cursor =Cursors.Wait;
            this.Close(); //closes the app
        }

        private void CounselorsMenuItem_Click(object sender, RoutedEventArgs e) //creates a counselor window
        {
            Maintenance frmMaintenance = new Maintenance(PersonType.COUNSELOR, ref db);

            
            frmMaintenance.ShowDialog();
            Refresh();
            
        }

        private void EmployeesMenuItem_Click(object sender, RoutedEventArgs e) //creates an employee window
        {
            Maintenance frmMaintenance = new Maintenance(PersonType.EMPLOYEE,ref db); 
           

           
            frmMaintenance.ShowDialog();
            Refresh();
          
        }

        private void RoomsMenuItem_Click(object sender, RoutedEventArgs e) //creates a room window
        {
            
            RoomWindow frmRoom = new RoomWindow(ref db);
           
            frmRoom.ShowDialog();
            Refresh();
           
        }

        private void ClientsMenuItem_Click(object sender, RoutedEventArgs e) //creates a client window
        {
            Maintenance frmMaintenance = new Maintenance(PersonType.CLIENT,ref db); 
            

           
            frmMaintenance.ShowDialog();
            Refresh();
          
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e) //closes the program
        {
            Cursor = Cursors.Wait;
            this.Close(); //closes the app
        }

        private void AppointmentMenuItem_Click(object sender, RoutedEventArgs e) //creates a new appointment window
        {
            Maintenance frmMaintenance = new Maintenance(PersonType.APPOINTMENT,ref db); //creates an appointment creation window

           
            frmMaintenance.ShowDialog();

            Refresh();
         
        }

        private void Refresh() //refreshes the data in the datagrid with appointments
        {
            Cursor = Cursors.Wait;
            var appointments1 = from a in db.ScheduledFors
                                orderby a.Appointment.StartTime
                                where DbFunctions.TruncateTime(a.Appointment.StartTime) == date
                                select new
                                {
                                    ApID = a.AppointmentID,
                                    Time = a.Appointment.StartTime,
                                    Duration = a.Appointment.Duration,
                                    Room = a.Appointment.Room.RoomName,
                                    Client = a.Client.Person.FirstName + " " + a.Client.Person.LastName,
                                    Counselor = a.Appointment.Counselor.Person.FirstName + " " + a.Appointment.Counselor.Person.LastName,
                                    Employee = a.Appointment.Employee.Person.FirstName + " " + a.Appointment.Employee.Person.LastName,
                                    Notes = a.Appointment.Notes,
                                    Kept = a.kept
                                };
            
            AppointmentDG.ItemsSource = appointments1.ToList();
            AppointmentDG.SelectedValuePath = "ApID";
            AppointmentDG.Columns[0].Visibility = System.Windows.Visibility.Hidden;
            AppointmentDG.SelectedIndex = 0;
            

            //sets whether the buttons are enabled based on if an item is selected
            if (AppointmentDG.Items.Count == 0)
            {
                editBtn.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
            else editBtn.IsEnabled = true;
            Cursor = Cursors.Arrow;
        }

        private void Cal_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) //updates the grid and delete button when a date is selected
        {
            date = Cal.SelectedDate;
            AppointmentDateTxt.Text = "Appointments for " + date.ToString();  //updates the header when clicking on a date.
            Refresh();
            if (date <= DateTime.Today || AppointmentDG.Items.Count == 0)
            {
                DeleteButton.IsEnabled = false;
                DeleteButton.ToolTip = "Due to HIPPA regulations, appointments in the past cannot be deleted";
            }
            else
            { DeleteButton.IsEnabled = true;
            DeleteButton.ToolTip = "Delete the selected appointment";
            }

            Mouse.Capture(null);

        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)//sets the ID column to invisible on load
        {
            AppointmentDG.Columns[0].Visibility = System.Windows.Visibility.Hidden;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) //deletes an appointment
        {
            
            {
                //gets the appointment from the datagrid
                Appointment thisAppointment = db.Appointments.Single(c1 => c1.AppointmentID == (int)AppointmentDG.SelectedValue);
                var scheduledfors = db.ScheduledFors.Where(s1 => s1.AppointmentID == (int)AppointmentDG.SelectedValue);

                //deletes it
                db.ScheduledFors.RemoveRange(scheduledfors);
                db.Appointments.Remove(thisAppointment);
                
                Cursor = Cursors.Wait;
                db.SaveChanges();
                Cursor = Cursors.Arrow;
                System.Windows.MessageBox.Show("Appointment was successfully deleted.");
                Refresh();

                
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e) //creates an edit appointment window
        {
            //gets the appointment
            Appointment thisAppt = db.Appointments.Single(a => a.AppointmentID == (int)AppointmentDG.SelectedValue);
            EditAppointment frmEditAppoinment = new EditAppointment(ref db, ref thisAppt);
            frmEditAppoinment.ShowDialog();
            Refresh();
        }


    }
}
