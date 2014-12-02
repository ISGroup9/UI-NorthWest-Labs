using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace NDFC
{

    /// 

    /// <summary>
    /// Interaction logic for Counselor.xaml
    /// This window is so that you can view the records, and select one to view/update it, or add a new record.
    /// </summary>
    
    public partial class Maintenance : Window
    {
        private PersonType type1;

        public Entities db;


       

        public Maintenance(PersonType type, ref Entities db1) //creates the maintance window based on the person type
        {
            db = db1;
            type1 = type;
            
            InitializeComponent();
            if (type == PersonType.COUNSELOR) //render the input boxes and text for counselors
            {
                this.Tag = "Counselors";
                DynamicTextBox1.Tag = "Degree Suffix";
                SelectFrame.Tag = "Counselor";
                DynamicTextBox2.Visibility = System.Windows.Visibility.Hidden;
                DefaultTextField2.Visibility = System.Windows.Visibility.Hidden;

                //fills in the list with counselors
                var Counselorlist = from c in db.Counselors orderby c.Person.LastName select new { c.CounselorID, c.Person.LastName, Name = c.Person.LastName + ", " + c.Person.FirstName };
                PersonName.ItemsSource = Counselorlist.ToList();
                PersonName.DisplayMemberPath = "Name";
                PersonName.SelectedValuePath = "CounselorID";
            }
            else if (type == PersonType.CLIENT) //render the input boxes and text for clients
            {
                //fills in the combo box with clients
                var clientlist1 = from c in db.Clients
                                 orderby c.Person.LastName
                                 select new { c.ClientID, Name = c.Person.LastName + ", " + c.Person.FirstName };
                ReferredByBox1.ItemsSource = clientlist1.ToList();
                ReferredByBox1.DisplayMemberPath = "Name";
                ReferredByBox1.SelectedValuePath = "ClientID";

                this.Tag = "Clients";
                DynamicTextBox1.Tag = "Reminder Method";
                Default1Box.ToolTip = "Select Reminder Method";
                //DynamicTextBox2.Tag = "Reminder Method";
                SelectFrame.Tag = "Client";

                //fills in the list with clients
                var clientlist = from c in db.Clients orderby c.Person.LastName select new { c.ClientID, c.Person.LastName, Name = c.Person.LastName + ", " + c.Person.FirstName };
                PersonName.ItemsSource = clientlist.ToList();
                PersonName.DisplayMemberPath = "Name";
                PersonName.SelectedValuePath = "ClientID";
                //DefaultTextField2.ToolTip = "Enter how to the client found out about New Dawn";
                ReferredByBox.Visibility = System.Windows.Visibility.Visible;
                ReferredByBox1.Visibility = System.Windows.Visibility.Visible;
            }
            else if (type == PersonType.EMPLOYEE) //render the input boxes and text for employees
            { 
                SupervisorBox.Text = "Supervisor: ";
                SupervisorBox1.ToolTip = "Optional: Select supervisor here";
                
                SupervisorBox.Visibility = System.Windows.Visibility.Visible;
                SupervisorBox1.Visibility = System.Windows.Visibility.Visible;
                this.Tag = "Employees";
                DynamicTextBox1.Tag = "Wage";
                Default1Box.ToolTip = "Enter Wage Here";
                SelectFrame.Tag = "Employee";
                HireDateSelector.Visibility = System.Windows.Visibility.Visible;
                HireDateText.Visibility = System.Windows.Visibility.Visible;
                DynamicTextBox2.Visibility = System.Windows.Visibility.Hidden;
                DefaultTextField2.Visibility = System.Windows.Visibility.Hidden;
      
             
                //fills in the list with employees
                var emplist1 = from e in db.Employees orderby e.Person.LastName select new {e.EmployeeID, e.Person.LastName, Name =e.Person.LastName + ", " + e.Person.FirstName};
                var emplist = emplist1.ToList();
                PersonName.ItemsSource = emplist;
                PersonName.DisplayMemberPath = "Name";
                PersonName.SelectedValuePath = "EmployeeID";
                SupervisorBox1.ItemsSource = emplist;
                SupervisorBox1.DisplayMemberPath = "Name";
                SupervisorBox1.SelectedValuePath = "EmployeeID";
                

            }
            else if (type == PersonType.APPOINTMENT)  //render the input boxes and text for appointments
            {
                //fills in the combo box with clients
                var clientlist1 = from c in db.Clients
                                 orderby c.Person.LastName
                                 select new { c.ClientID, Name = c.Person.LastName + ", " + c.Person.FirstName };
                ReferredByBox1.ItemsSource = clientlist1.ToList();
                ReferredByBox1.DisplayMemberPath = "Name";
                ReferredByBox1.SelectedValuePath = "ClientID";
                ReferredByBox1.ToolTip = "Enter Referring Client";
                Default1Box.ToolTip = "Select Reminder Method";
                ReferredByBox.Visibility = System.Windows.Visibility.Visible;
                ReferredByBox1.Visibility = System.Windows.Visibility.Visible;
               
                this.Tag = "Create Appointment";
                DynamicTextBox1.Tag = "Reminder Method";
                //DynamicTextBox2.Tag = "Reminder Method";
                SelectFrame.Tag = "Client";
                NewAppTab.Visibility = System.Windows.Visibility.Visible;
                NewAppTab.IsSelected = true;
                UpdateTab.Header += " Client";
                NewTab.Header += " Client";

                //fills in the list and the combo box with clients
                var clientlist = from c in db.Clients orderby c.Person.LastName select new { c.ClientID, c.Person.LastName, Name = c.Person.LastName + ", " + c.Person.FirstName };
                PersonName.ItemsSource = clientlist.ToList();
                PersonName.DisplayMemberPath = "Name";
                PersonName.SelectedValuePath = "ClientID";
                var clientboxlist = from c in db.Clients
                                 orderby c.Person.LastName
                                 select new { c.ClientID, Name = c.Person.LastName + ", " + c.Person.FirstName };
                Clientbox.ItemsSource = clientboxlist.ToList();
                Clientbox.DisplayMemberPath = "Name";
                Clientbox.SelectedValuePath = "ClientID";
                RoomBox.ToolTip = "Please select a date and time first";
                //var roomlist =   from c in db.Rooms
                  //                  select new { c.RoomNumber, Name = c.RoomName };
                //RoomBox.ItemsSource = roomlist.ToList();
                //RoomBox.DisplayMemberPath = "Name";
                //RoomBox.SelectedValuePath = "RoomNumber";

                //fills in the combo box with counselors
                var counselorboxlist = from c in db.Counselors
                                    orderby c.Person.LastName
                                    select new { c.CounselorID, Name = c.Person.LastName + ", " + c.Person.FirstName };
                CounselorBox.ItemsSource = counselorboxlist.ToList();
                CounselorBox.DisplayMemberPath = "Name";
                CounselorBox.SelectedValuePath = "CounselorID";


                //fills in the combo box with counselors
                var employeeboxlist = from c in db.Employees
                                       orderby c.Person.LastName
                                       select new { c.EmployeeID, Name = c.Person.LastName + ", " + c.Person.FirstName };
                EmployeeBox.ItemsSource = employeeboxlist.ToList();
                EmployeeBox.DisplayMemberPath = "Name";
                EmployeeBox.SelectedValuePath = "EmployeeID";

               // NewAppTab.d
            }
            PersonName.SelectedIndex = 0;
            //renders the tags created by the if statementes.
            DynamicTextBox1.Text = (string)DynamicTextBox1.Tag + ":";
            this.Title = (string)this.Tag;
            SelectFrame.Content = "Select " + (string)SelectFrame.Tag;
        }
        
        private void UpdateButton_Click(object sender, RoutedEventArgs e) // creates a new update window on update button click
        {
            
            if (type1 == PersonType.EMPLOYEE) //Creates an employee update window 
            {
                Employee thisEmployee = db.Employees.Single(e1 => e1.EmployeeID == (int)PersonName.SelectedValue);
               
                UpdateEmployee frmUpdate = new UpdateEmployee(type1, ref thisEmployee, ref db);
                frmUpdate.ShowDialog();

                //refreshes the list
                var emplist = from e2 in db.Employees orderby e2.Person.LastName select new { e2.EmployeeID, Name = e2.Person.LastName + ", " + e2.Person.FirstName };
                PersonName.ItemsSource = emplist.ToList();
                PersonName.DisplayMemberPath = "Name";
                PersonName.SelectedValuePath = "EmployeeID";
            }
            else if (type1 == PersonType.CLIENT || type1 == PersonType.APPOINTMENT) // creates a client update window
            {
                Client thisClient = db.Clients.Single(c1 => c1.ClientID == (int)PersonName.SelectedValue);
                UpdateClient frmUpdate = new UpdateClient(ref thisClient, ref db);
                frmUpdate.ShowDialog();


                //refreshes the list
                var clientlist = from c in db.Clients orderby c.Person.LastName select new { c.ClientID,  Name = c.Person.LastName + ", " + c.Person.FirstName };
                PersonName.ItemsSource = clientlist.ToList();
                PersonName.DisplayMemberPath = "Name";
                PersonName.SelectedValuePath = "ClientID";
            }

            else if (type1 == PersonType.COUNSELOR) //creates a counselor update window
            {
                Counselor thisCounselor = db.Counselors.Single(c1 => c1.CounselorID == (int)PersonName.SelectedValue);
                UpdateCounselor frmUpdate = new UpdateCounselor(ref thisCounselor, ref db);
                frmUpdate.ShowDialog();


                //refreshes the list
                var Counselorlist = from cl in db.Counselors orderby cl.Person.LastName select new { cl.CounselorID, Name = cl.Person.LastName + ", " + cl.Person.FirstName };
                PersonName.ItemsSource = Counselorlist.ToList();
                PersonName.DisplayMemberPath = "Name";
                PersonName.SelectedValuePath = "CounselorID";
                
            }
            PersonName.SelectedIndex = 0;
            
        }


        //adds an appointment based on the data supplied
        private void AddAppButton_Click(object sender, RoutedEventArgs e) //closes the window onces the appointment gets added
        {
            if (DateBox.SelectedDate != null) //checks to see if the date is selected
            {
                if (CounselorBox.SelectedIndex != -1 && RoomBox.SelectedIndex != -1 && Clientbox.SelectedIndex != -1 && EmployeeBox.SelectedIndex != -1) //checks to make sure the user selected items from all the combo boxes
                {
                    try
                    {

                        Appointment newAppointment = new Appointment();
                        newAppointment.CounselorID = (int)CounselorBox.SelectedValue;
                        newAppointment.EmployeeID = (int)EmployeeBox.SelectedValue;

                        newAppointment.RoomNumber = (int)RoomBox.SelectedValue;
                        ScheduledFor scheduledFor = new ScheduledFor();
                        scheduledFor.ClientID = (int)Clientbox.SelectedValue;
                        newAppointment.ScheduledFors.Add(scheduledFor);
                        try
                        {
                        newAppointment.Notes = NotesBox.Text;
                        }
                        catch (System.FormatException)
                        {
                            System.Windows.MessageBox.Show("Invalid Time Format. Use mm:hh");
                            throw new System.FormatException();
                        }

                        DateTime? timeSpan = timePicker.Value;

                        TimeSpan time = timeSpan.Value.TimeOfDay;

                        
                        DateTime? date = DateBox.SelectedDate;
                        //DateTime? date = datePicker.Value;
                         newAppointment.StartTime = date + time;
                        
                       
                        try
                        {
                            newAppointment.Duration = int.Parse(DurationBox.Text);
                        }
                        catch (System.FormatException)
                        {
                            System.Windows.MessageBox.Show("Invalid Duration. Use format: mm");
                            throw new System.FormatException();
                        }
                        //db.Appointments.Add(newAppointment);

                        var update = db.Database.ExecuteSqlCommand("insert into appointment(starttime,duration, counselorid, employeeid,  roomnumber,notes) values(@ST,@DUR,@COI,@EID,@RN,@NT); insert into scheduledfor values((select max(appointmentid) from appointment),@CLIID,null,null);",
                        
                        
                        new SqlParameter("ST", newAppointment.StartTime.ToString()),
                        new SqlParameter("DUR", newAppointment.Duration.ToString()),
                        new SqlParameter("COI", newAppointment.CounselorID.ToString()),
                        new SqlParameter("EID", newAppointment.EmployeeID.ToString()),
                        new SqlParameter("RN", newAppointment.RoomNumber.ToString()),
                        new SqlParameter("CLIID", scheduledFor.ClientID.ToString()),
                        new SqlParameter("NT",newAppointment.Notes.ToString())
                        );
                        db.SaveChanges();
                        this.Close();
                        System.Windows.MessageBox.Show("New Appointment Created");
                    }
                    catch (System.FormatException) { }
                }
                else  System.Windows.MessageBox.Show("Please select a counselor, room, employee, and client");
            }
            else  System.Windows.MessageBox.Show("Please select a date");
            
        }

        //creates a new record
        private void AddNewButton_Click(object sender, RoutedEventArgs e) //goes back to the main tab after adding a record
        {
            try
            {
                
                if (type1 == PersonType.EMPLOYEE) // creates a new employee
                {
                    Employee emp1 = new Employee();
                    Person pers1 = new Person();
                    pers1.FirstName = FNameBox.Text;
                    pers1.LastName = LNameBox.Text;
                    pers1.City = CityBox.Text;
                    
                        pers1.State = (string)StateBox.SelectionBoxItem;
                    
                    
                    try
                    {
                        pers1.ZIP = int.Parse(ZIPBox.Text).ToString();
                    }
                    catch
                    {
                         System.Windows.MessageBox.Show("ZIP Codes must only contain digits");
                        throw new System.FormatException();
                    }
                        pers1.Phone = PhoneBox.Text;
                        pers1.Email = EmailBox.Text;
                    
                    try
                    {
                        emp1.Wage = decimal.Parse(Default1Box.Text);
                    }
                    catch
                    {
                         System.Windows.MessageBox.Show("Wages must be a decimal number");
                        throw new System.FormatException();
                    }
                    emp1.DateHired = HireDateSelector.SelectedDate;
                    pers1.State = StateBox.SelectionBoxItem.ToString();
                    emp1.Person = pers1;
                    
                    


                    //db.Employees.Add(emp1);

                    //Creates using SQL
                    db.Database.ExecuteSqlCommand("Insert Into Person(firstname,lastname,city,state,zip,phone,email) values (@firstname,@lastname,@city,@state,@zip,@phone,@email);",
                    new SqlParameter("firstname", pers1.FirstName),
                    new SqlParameter("LastName", pers1.LastName),
                    new SqlParameter("City", pers1.City),
                    new SqlParameter("State", pers1.State),
                    new SqlParameter("ZIP", pers1.ZIP),
                    new SqlParameter("Phone", pers1.Phone),
                    new SqlParameter("Email", pers1.Email));

                    if (SupervisorBox1.SelectedIndex != -1)
                    {
                        emp1.SupervisorID = (int)SupervisorBox1.SelectedValue;
                        db.Database.ExecuteSqlCommand("insert into Employee values((select max(personid) from person), @HireDate,@Wage, @SupervisorID);",
                         new SqlParameter("HireDate", emp1.DateHired.ToString()),
                         new SqlParameter("Wage", emp1.Wage),
                         new SqlParameter("SupervisorID", emp1.SupervisorID));

                    }
                    else
                    {
                        db.Database.ExecuteSqlCommand("insert into Employee values((select max(personid) from person), @HireDate,@Wage, ((select max(personid) from person));",
                        new SqlParameter("HireDate", emp1.DateHired.ToString()),
                        new SqlParameter("Wage", emp1.Wage));
                    }



                    db.SaveChanges();
                    UpdateTab.IsSelected = true;

                    //refreshes the list
                    var emplist = from e2 in db.Employees orderby e2.Person.LastName select new { e2.EmployeeID, Name = e2.Person.LastName + ", " + e2.Person.FirstName };
                    PersonName.ItemsSource = emplist.ToList();
                    PersonName.DisplayMemberPath = "Name";
                    PersonName.SelectedValuePath = "EmployeeID";
                    PersonName.SelectedIndex = 0;

                    
                }
                else if (type1 == PersonType.COUNSELOR) //creates a new counselor
                {
                    Counselor counselor = new Counselor();
                    Person pers1 = new Person();
                    pers1.FirstName = FNameBox.Text;
                    pers1.LastName = LNameBox.Text;
                    pers1.City = CityBox.Text;

                    pers1.State = (string)StateBox.SelectionBoxItem;


                    try
                    {
                        pers1.ZIP = int.Parse(ZIPBox.Text).ToString();
                    }
                    catch
                    {
                         System.Windows.MessageBox.Show("ZIP Codes must only contain digits");
                        throw new System.FormatException();
                    }
                    pers1.Phone = PhoneBox.Text;
                    pers1.Email = EmailBox.Text;
                    

                    counselor.Person = pers1;

                    counselor.DegreeSuffix = Default1Box.Text;

                    //db.Counselors.Add(counselor);


                    //runs sql commands
                    db.Database.ExecuteSqlCommand("Insert Into Person(firstname,lastname,city,state,zip,phone,email) values (@firstname,@lastname,@city,@state,@zip,@phone,@email);",
                    new SqlParameter("firstname", pers1.FirstName),
                    new SqlParameter("LastName", pers1.LastName),
                    new SqlParameter("City", pers1.City),
                    new SqlParameter("State", pers1.State),
                    new SqlParameter("ZIP", pers1.ZIP),
                    new SqlParameter("Phone", pers1.Phone),
                    new SqlParameter("Email", pers1.Email));

                    db.Database.ExecuteSqlCommand("insert into counselor values((select max(personid) from person), @Suffix);",
                        new SqlParameter("Suffix",counselor.DegreeSuffix));


                    db.SaveChanges();
                    UpdateTab.IsSelected = true;

                    //refreshes
                    var Counselorlist = from cl in db.Counselors orderby cl.Person.LastName select new { cl.CounselorID, Name = cl.Person.LastName + ", " + cl.Person.FirstName };
                    PersonName.ItemsSource = Counselorlist.ToList();
                    PersonName.DisplayMemberPath = "Name";
                    PersonName.SelectedValuePath = "CounselorID";
                    PersonName.SelectedIndex = 0;
                }
                else if (type1 == PersonType.CLIENT || type1 == PersonType.APPOINTMENT) // creates a new client
                {
                    
                    Client client = new Client();
                    Person pers1 = new Person();
                    pers1.FirstName = FNameBox.Text;
                    pers1.LastName = LNameBox.Text;
                    pers1.City = CityBox.Text;

                    pers1.State = (string)StateBox.SelectionBoxItem;


                    try
                    {
                        pers1.ZIP = int.Parse(ZIPBox.Text).ToString();
                    }
                    catch
                    {
                         System.Windows.MessageBox.Show("ZIP Codes must only contain digits");
                        throw new System.FormatException();
                    }
                    pers1.Phone = PhoneBox.Text;
                    pers1.Email = EmailBox.Text;
                    client.ReminderMethod = Default1Box.Text;
                    
                    //runs the SQL command
                    db.Database.ExecuteSqlCommand("Insert Into Person(firstname,lastname,city,state,zip,phone,email) values (@firstname,@lastname,@city,@state,@zip,@phone,@email);",
                    new SqlParameter("firstname", pers1.FirstName),
                    new SqlParameter("LastName", pers1.LastName),
                    new SqlParameter("City", pers1.City),
                    new SqlParameter("State", pers1.State),
                    new SqlParameter("ZIP", pers1.ZIP),
                    new SqlParameter("Phone", pers1.Phone),
                    new SqlParameter("Email", pers1.Email),
                    new SqlParameter("foundout", DefaultTextField2.Text));

                    if (ReferredByBox1.SelectedIndex != -1)
                    {
                       db.Database.ExecuteSqlCommand("Insert into Client values((select max(personid) from person),@foundout, @ReminderMethod, @ReferredBy);",
                      new SqlParameter("ReminderMethod", client.ReminderMethod),
                      new SqlParameter("ReferredBy", (int)ReferredByBox1.SelectedValue),
                      new SqlParameter("foundout", DefaultTextField2.Text));
                    }
                    else
                    {
                       db.Database.ExecuteSqlCommand("Insert into Client values((select max(personid) from person),@foundout, @ReminderMethod, null);",
                     new SqlParameter("ReminderMethod", client.ReminderMethod),
                     new SqlParameter("foundout", DefaultTextField2.Text));
                    }

                    //db.Clients.Add(client);
                    db.SaveChanges();
                    UpdateTab.IsSelected = true;

                    //refreshes the list
                    var clientlist = from c in db.Clients orderby c.Person.LastName select new { c.ClientID, Name = c.Person.LastName + ", " + c.Person.FirstName };
                    PersonName.ItemsSource = clientlist.ToList();
                    PersonName.DisplayMemberPath = "Name";
                    PersonName.SelectedValuePath = "ClientID";
                    PersonName.SelectedIndex = 0;

                    
                }

                //clears all the text fields and combo boxes so that it can be used to create another record
                FNameBox.Clear();
                LNameBox.Clear();
                CityBox.Clear();
                StateBox.SelectedIndex = -1;
                ZIPBox.Clear();
                PhoneBox.Clear();
                EmailBox.Clear();
                Default1Box.Clear();
                DefaultTextField2.Clear();
                ReferredByBox1.SelectedIndex = -1;
                SupervisorBox1.SelectedIndex = -1;
                System.Windows.MessageBox.Show("Record Successfully Added");
                
            }
            catch (System.FormatException) {  };
        }
            
        

        private void CloseBtn_Click(object sender, RoutedEventArgs e) // closes the window
        {
            this.Close();
        }

       

        private void DeleteBtn_Click(object sender, RoutedEventArgs e) //deletes a selected record
        {
            if (type1 == PersonType.CLIENT || type1 == PersonType.APPOINTMENT) // deletes a client
            {

                //check to make sure that the client doesn't have any appointments
                var countofapps = (from c in db.ScheduledFors where c.ClientID == (int)PersonName.SelectedValue select c).Count();
                //var sqlCnt = "SELECT count(*) FROM appointment where EmployeeID = " + txtBoxID.Text;
                //var queryResult = _context.Database.SqlQuery<int>(sqlCnt).Single();

                if (countofapps > 0)
                {
                     System.Windows.MessageBox.Show("Due to HIPPA regulations, this record cannot be deleted.");
                }
                else
                {

                    Client thisClient = db.Clients.Single(c1 => c1.ClientID == (int)PersonName.SelectedValue);
                    
                    db.Clients.Remove(thisClient);
                    //db.Database.ExecuteSqlCommand("Delete from Employee Where EmployeeID =" + txtBoxID.Text);
                    //db.Database.ExecuteSqlCommand("Delete from Person Where PersonID =" + txtBoxID.Text);
                    db.SaveChanges();
                     System.Windows.MessageBox.Show("Client was successfully deleted.");


                    //refreshes the list
                    var clientlist = from c in db.Clients orderby c.Person.LastName select new { c.ClientID, Name = c.Person.LastName + ", " + c.Person.FirstName };
                    PersonName.ItemsSource = clientlist.ToList();
                    PersonName.DisplayMemberPath = "Name";
                    PersonName.SelectedValuePath = "ClientID";
                    PersonName.SelectedIndex = 0;
                }
            }

            if (type1 == PersonType.EMPLOYEE) // deletes an employee record
            {
                //checks to make sure the employee isn't a supervior and that the employee doesn't have an appointment assciated
                var countofSupervised = db.Employees.Count(e1 => e1.SupervisorID == (int)PersonName.SelectedValue);
                var countofapps = (from c in db.Appointments where c.EmployeeID == (int)PersonName.SelectedValue select c).Count();
                if (countofSupervised > 1)
                {
                    System.Windows.MessageBox.Show("This employee supervises others, this record cannot be deleted.");
                }
                else
                {
                    if (countofapps > 0)
                    {
                        System.Windows.MessageBox.Show("Due to HIPPA regulations, this record cannot be deleted.");
                    }
                    else
                    {
                        Employee thisEmployee = db.Employees.Single(c1 => c1.EmployeeID == (int)PersonName.SelectedValue);

                        db.Employees.Remove(thisEmployee);
                        db.SaveChanges();
                        System.Windows.MessageBox.Show("Employee was successfully deleted.");

                        var clientlist = from c in db.Employees orderby c.Person.LastName select new { c.EmployeeID, Name = c.Person.LastName + ", " + c.Person.FirstName };
                        PersonName.ItemsSource = clientlist.ToList();
                        PersonName.DisplayMemberPath = "Name";
                        PersonName.SelectedValuePath = "EmployeeID";
                        PersonName.SelectedIndex = 0;
                    }
                }
            }

            if (type1 == PersonType.COUNSELOR) //deletes a counselor
            {
                //checks to make sure the counselor doesn't have any appointments
                var countofapps = (from c in db.Appointments where c.CounselorID == (int)PersonName.SelectedValue select c).Count();

                if (countofapps > 0)
                {
                     System.Windows.MessageBox.Show("Due to HIPPA regulations, this record cannot be deleted.");
                }
                else
                {
                    Counselor thisCounselor = db.Counselors.Single(c1 => c1.CounselorID == (int)PersonName.SelectedValue);

                    db.Counselors.Remove(thisCounselor);
                    db.SaveChanges();
                     System.Windows.MessageBox.Show("Counselor was successfully deleted.");

                    //refreshes the list

                    var clientlist = from c in db.Counselors orderby c.Person.LastName select new { c.CounselorID, Name = c.Person.LastName + ", " + c.Person.FirstName };
                    PersonName.ItemsSource = clientlist.ToList();
                    PersonName.DisplayMemberPath = "Name";
                    PersonName.SelectedValuePath = "CounselorID";
                    PersonName.SelectedIndex = 0;
                }
            }




        }

        private void NotesBox_KeyUp(object sender, KeyEventArgs e) //Adds the appointment when enter key is pressed from the notes box
        {
            if (e.Key == Key.Enter)
            {
                AddAppButton_Click(sender, e);
            }
        }

        private void CounselorBox_SelectionChanged(object sender, SelectionChangedEventArgs e) // updates the appointments grid and the rooms in the combo box when the combo box selection is changed
        {
            
            if (CounselorBox.SelectedIndex != -1 && DateBox.SelectedDate != null)
            {
                UpdateAppointments();
            }
            UpdateRooms();
        }

        private void UpdateRooms() // updates the combo box to show only available rooms for a given time, date, and duration
        {
            if (DateBox.SelectedDate != null && timePicker.Value != null) // checks to make sure the date and time are selected
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

                //update the combo box
                var roomlist = from c in db.Rooms
                               where c.Appointments.Where(a => (a.StartTime >= newstarttime && a.StartTime < newendtime) || (newstarttime  >= a.StartTime && newstarttime < DbFunctions.AddMinutes(a.StartTime, a.Duration))).Count() == 0
                               select new { c.RoomNumber, Name = c.RoomName };
                RoomBox.ItemsSource = roomlist.ToList();
                RoomBox.DisplayMemberPath = "Name";
                RoomBox.SelectedValuePath = "RoomNumber";

                Cursor = Cursors.Arrow;
            }
            
            
        }

        private void UpdateAppointments() //updates the appointments data grid based on the counselor and date selected
        {
            Cursor = Cursors.Wait;
            DateTime? date1 =  DateBox.SelectedDate + TimeSpan.Parse("1");
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

        private void DateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e) // updates the appointments grid and the rooms in the combo box when the date is changed
        {
            if (CounselorBox.SelectedIndex != -1 && DateBox.SelectedDate != null)
            {
                
                UpdateAppointments();
              
            }
            UpdateRooms();
        }

        private void timePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e) // updates the rooms in the combo box when the time is changed
        {
            UpdateRooms();
        }

        private void DurationBox_LostFocus(object sender, RoutedEventArgs e) // updates the rooms in the combo box when the duration is changed
        {
            UpdateRooms();
        }

     
       

    }
}
