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

namespace NDFC
{
    /// <summary>
    /// Interaction logic for UpdateClient.xaml
    /// This window is so that you can view/update the info for a single record in the system
    /// </summary>
    public partial class UpdateClient : Window
    {

        public Entities db;

       
       
        private Client client1;
        

        public UpdateClient(ref Client client, ref Entities db1)//initizizes the data into the boxes
        {

            Cursor = Cursors.Wait;
            db = db1;
            client1 = client;

            InitializeComponent();



            FoundOutBox.Text = client1.FoundOut;
            FNameBox.Text = client1.Person.FirstName;
            LNameBox.Text = client1.Person.LastName;
            CityBox.Text = client1.Person.City;
           
            

            int i;
            for (i = 0; i < StateBox.Items.Count+1; i++)
            {
                StateBox.SelectedIndex = i;
                if(StateBox.SelectionBoxItem.ToString() == client1.Person.State){
                    break;
                }
            }

            if (i > StateBox.Items.Count)
            {
                StateBox.SelectedIndex = -1;
            }
            
            ZipBox.Text = client1.Person.ZIP;
            PhoneBox.Text = client1.Person.Phone;
            EmailBox.Text = client1.Person.Email;

            var clientlist = from c in db.Clients 
                             orderby c.Person.LastName 
                             select new { c.ClientID, Name = c.Person.LastName + ", " + c.Person.FirstName };
            ReferredByBox1.ItemsSource = clientlist.ToList();
            ReferredByBox1.DisplayMemberPath = "Name";
            ReferredByBox1.SelectedValuePath = "ClientID";

            ReferredByBox1.SelectedValue = client1.ReferredByClientID;

            /*for (i = 0; i < ReferredByBox1.Items.Count-1; i++)
            {
                StateBox.SelectedIndex = i;
                if (StateBox.S == client1.Person.State)
                {
                    break;
                }
            }*/
            
            DefaultTextField2.Text = client1.ReminderMethod;
            //Email

            var appointments = from a in db.ScheduledFors 
                               orderby a.Appointment.StartTime 
                               where a.ClientID == client1.ClientID 
                               select new {
                                   Time = a.Appointment.StartTime,
                                   Duration = a.Appointment.Duration,
                                   Room = a.Appointment.Room.RoomName, 
                                   Counselor = a.Appointment.Counselor.Person.FirstName + " " + a.Appointment.Employee.Person.LastName, 
                                   Employee = a.Appointment.Employee.Person.FirstName + " " + a.Appointment.Employee.Person.LastName ,
                                   Notes = a.Appointment.Notes,
                                   Kept = a.kept
                               };
            AppointmentGrid.ItemsSource = appointments.ToList();

            //AppointmentGrid.Column[2].Header = 

            InitializeComponent();
            Cursor = Cursors.Arrow;
           
        }

       
       
        private void Update1Button_Click(object sender, RoutedEventArgs e) //updates the client
        {
            try
            {
                

                client1.Person.FirstName = FNameBox.Text;
                    client1.Person.LastName = LNameBox.Text;
                     client1.Person.City = CityBox.Text;
                     client1.FoundOut = FoundOutBox.Text;
                     client1.Person.State = StateBox.SelectionBoxItem.ToString();
                     try
                     {
                         client1.Person.ZIP = int.Parse(ZipBox.Text).ToString();
                     }
                     catch
                     {
                         MessageBox.Show("ZIP Codes must only contain digits");
                         throw new System.FormatException();
                     } 
                     client1.Person.Phone = PhoneBox.Text;
                     client1.Person.Email = EmailBox.Text;
                    client1.ReminderMethod = DefaultTextField2.Text;
                if(ReferredByBox1.SelectedIndex != -1)
                     client1.ReferredByClientID = (int)ReferredByBox1.SelectedValue;

                


                
                db.SaveChanges();
                this.Close();
            }
            catch (System.FormatException) { MessageBox.Show("Incorrect Format"); }
            catch (System.Data.Entity.Validation.DbEntityValidationException) { MessageBox.Show("Incorrect Format"); }
        }



        private void CloseButton_Click(object sender, RoutedEventArgs e) //closes the window
        {
            this.Close();
        }
    }
}
