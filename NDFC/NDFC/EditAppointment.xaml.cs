using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace NDFC
{
    /// <summary>
    /// Interaction logic for EditAppointment.xaml
    /// </summary>
    public partial class EditAppointment : Window
    {
        public Entities db;
        private Appointment app;
        public EditAppointment(ref Entities db1, ref Appointment app1) // constructor method. Creates the window
        {
            Cursor = Cursors.Wait;
            db = db1;
            app = app1;
            InitializeComponent();

            //selects all the clients, puts them in the client combobox
            var clientboxlist = from c in db.Clients
                                orderby c.Person.LastName
                                select new { c.ClientID, Name = c.Person.LastName + ", " + c.Person.FirstName };
            Clientbox.ItemsSource = clientboxlist.ToList();
            Clientbox.DisplayMemberPath = "Name";
            Clientbox.SelectedValuePath = "ClientID";
            Clientbox.SelectedValue = app.ScheduledFors.First().ClientID;
            Clientbox.IsReadOnly = true;
            Clientbox.IsEnabled = false;

            //selects all the clients, puts them in the counselor combobox
            var counselorboxlist = from c in db.Counselors
                                   orderby c.Person.LastName
                                   select new { c.CounselorID, Name = c.Person.LastName + ", " + c.Person.FirstName };
            CounselorBox.ItemsSource = counselorboxlist.ToList();
            CounselorBox.DisplayMemberPath = "Name";
            CounselorBox.SelectedValuePath = "CounselorID";
            CounselorBox.SelectedValue = app.CounselorID;

            //selects all the clients, puts them in the client combobox
            var employeeboxlist = from c in db.Employees
                                  orderby c.Person.LastName
                                  select new { c.EmployeeID, Name = c.Person.LastName + ", " + c.Person.FirstName };
            EmployeeBox.ItemsSource = employeeboxlist.ToList();
            EmployeeBox.DisplayMemberPath = "Name";
            EmployeeBox.SelectedValuePath = "EmployeeID";
            EmployeeBox.SelectedValue = app.EmployeeID;

            //sets the date box to the Appointments date
            DateBox.SelectedDate = app.StartTime.Value.Date;

            //sets the timepick to the Appointments time
            timePicker.Value = new DateTime() + app.StartTime.Value.TimeOfDay;

            //sets the duration to the Appoinments duration
            DurationBox.Text = app.Duration.ToString();
            NotesBox.Text = app.Notes;
            KeptBox.IsChecked = app.ScheduledFors.First().kept;

            //sets the rooms combo box from the rooms.
            var roomlist = from c in db.Rooms
                           select new { c.RoomNumber, Name = c.RoomName };
            RoomBox.ItemsSource = roomlist.ToList();
            RoomBox.DisplayMemberPath = "Name";
            RoomBox.SelectedValuePath = "RoomNumber";

            SetRooms();
            RoomBox.SelectedValue = app.RoomNumber;

            SetAppointments();
           
            
            Cursor = Cursors.Arrow;
        }
        private void UpdateRooms() //updates the rooms available in the combo box based on room availability
        {
            if (DateBox.SelectedDate != null && timePicker.Value != null) //makes sure that the date combo box and the time picker are set
            {
                RoomBox.ToolTip = "Select Room";
                Cursor = Cursors.Wait;
                DateTime? timeSpan = timePicker.Value;

                TimeSpan time = timeSpan.Value.TimeOfDay;


                DateTime? date = DateBox.SelectedDate;
                DateTime? newstarttime = date + time;
                int duration;
                if (int.TryParse(DurationBox.Text, out duration))
                {
                }
                else duration = 1;
                DateTime? newendtime = newstarttime.Value.AddMinutes(duration);
                var roomlist = from c in db.Rooms
                               where c.Appointments.Count(a => ((a.StartTime >= newstarttime && a.StartTime < newendtime) || (newstarttime >= a.StartTime && newstarttime < DbFunctions.AddMinutes(a.StartTime, a.Duration))) && a.AppointmentID != app.AppointmentID) == 0
                               select new { c.RoomNumber, Name = c.RoomName };
                RoomBox.ItemsSource = roomlist.ToList();
                RoomBox.DisplayMemberPath = "Name";
                RoomBox.SelectedValuePath = "RoomNumber";

                Cursor = Cursors.Arrow;
            }
        }
        private void SetRooms() //sets the rooms available in the combo box based on room availability
        {
            
                RoomBox.ToolTip = "Select Room";
                Cursor = Cursors.Wait;
                
               
                DateTime? newstarttime = app.StartTime;
              
                
               
                DateTime? newendtime = newstarttime.Value.AddMinutes((double)app.Duration);
               // var applist = db.Appointments.Where(a => (a.StartTime >= newstarttime && a.StartTime < newendtime) || (newstarttime >= a.StartTime && newstarttime < DbFunctions.AddMinutes(a.StartTime, a.Duration)));
                //var applist1 = applist.Where(a => a.AppointmentID != app.AppointmentID);
                var roomlist = from c in db.Rooms
                               where c.Appointments.Count(a =>  ( (a.StartTime >= newstarttime && a.StartTime < newendtime) || (newstarttime >= a.StartTime && newstarttime < DbFunctions.AddMinutes(a.StartTime, a.Duration)) ) && a.AppointmentID != app.AppointmentID ) == 0
                               select new { c.RoomNumber, Name = c.RoomName };
                RoomBox.ItemsSource = roomlist.ToList();
                RoomBox.DisplayMemberPath = "Name";
                RoomBox.SelectedValuePath = "RoomNumber";

                Cursor = Cursors.Arrow;
            
        }
        
        private void UpdateAppointments() //updates the appointments data grid based on the counselor and date selected
        {
            Cursor = Cursors.Wait;
            DateTime? date1 = DateBox.SelectedDate + TimeSpan.Parse("1");
            var appointments1 = from a in db.ScheduledFors
                                orderby a.Appointment.StartTime
                                where a.Appointment.StartTime >= DateBox.SelectedDate && a.Appointment.StartTime < date1 && a.Appointment.CounselorID == (int)CounselorBox.SelectedValue
                                select new
                                {
                                    
                                    Time = a.Appointment.StartTime,
                                    Duration = a.Appointment.Duration,
                                    
                                };
            dgApt.ItemsSource = appointments1.ToList();
            Cursor = Cursors.Arrow;

        }
        private void SetAppointments() //sets the appointments data grid based on the counselor and date selected
        {
            Cursor = Cursors.Wait;
            DateTime? date2 = app.StartTime.Value.Date + TimeSpan.Parse("1");
            DateTime? date1 = app.StartTime.Value.Date;

            var appointments1 = from a in db.ScheduledFors
                                orderby a.Appointment.StartTime
                                where a.Appointment.StartTime >= date1 && a.Appointment.StartTime < date2 && a.Appointment.CounselorID == app.CounselorID
                                select new
                                {
                                
                                    Time = a.Appointment.StartTime,
                                    Duration = a.Appointment.Duration,
                                   
                                };
            dgApt.ItemsSource = appointments1.ToList();
            Cursor = Cursors.Arrow;

        }

        private void DateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e) //when the selected date is changed the datagrid and the rooms combo box are updated
        {
            if (CounselorBox.SelectedIndex != -1 && DateBox.SelectedDate != null)
            {

                UpdateAppointments();

            }
            UpdateRooms();
        }

        private void timePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e) //when the time is changed rooms combo box is updated
        {
            UpdateRooms();
        }

        private void DurationBox_LostFocus(object sender, RoutedEventArgs e) //when the duration is changed rooms combo box is updated
        {
            UpdateRooms();
        }
        private void CounselorBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //when the counselorbox is changed the datagrid and the rooms combo box are updated
        {

            if (CounselorBox.SelectedIndex != -1 && DateBox.SelectedDate != null)
            {
                UpdateAppointments();
            }
            UpdateRooms();
        }
        private void NotesBox_KeyUp(object sender, KeyEventArgs e) // when the enter key is pressed from the notes box, the add button gets clicked
        {
            if (e.Key == Key.Enter)
            {
                AddAppButton_Click(sender, e);
            }
        }

        //edits the appointment based on the supplied information.
        private void AddAppButton_Click(object sender, RoutedEventArgs e) //closes the window onces the appointment gets edited
        {
            if (DateBox.SelectedDate != null) //checks to make sure there is a date selected
            {
                if (CounselorBox.SelectedIndex != -1 && RoomBox.SelectedIndex != -1 && Clientbox.SelectedIndex != -1 && EmployeeBox.SelectedIndex != -1) //checks to make sure the user selected items from all the combo boxes
                {
                    try
                    {

                        
                        app.CounselorID = (int)CounselorBox.SelectedValue;
                        app.EmployeeID = (int)EmployeeBox.SelectedValue;

                        app.RoomNumber = (int)RoomBox.SelectedValue;
                        app.ScheduledFors.First().ClientID = (int)Clientbox.SelectedValue;
                        
                       
                            app.Notes = NotesBox.Text;
                        
                        

                        DateTime? timeSpan = timePicker.Value;

                        TimeSpan time = timeSpan.Value.TimeOfDay;


                        DateTime? date = DateBox.SelectedDate;
                        //DateTime? date = datePicker.Value;
                        app.StartTime = date + time;
                        app.ScheduledFors.First().kept = KeptBox.IsChecked;

                        try
                        {
                            app.Duration = int.Parse(DurationBox.Text);
                        }
                        catch (System.FormatException)
                        {
                            System.Windows.MessageBox.Show("Invalid Duration. Use format: mm");
                            throw new System.FormatException();
                        }

                        
                        db.SaveChanges();
                        this.Close();
                        System.Windows.MessageBox.Show("Appointment Updated");
                    }
                    catch (System.FormatException) { }
                }
                else System.Windows.MessageBox.Show("Please select a counselor, room, employee, and client");
            }
            else System.Windows.MessageBox.Show("Please select a date");

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void dgApt_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
