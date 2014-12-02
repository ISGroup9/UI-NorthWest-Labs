using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
    /// Interaction logic for Room.xaml
    /// This creates a select window for rooms so that you can go in and update them or add more rooms.
    /// </summary>
    public partial class RoomWindow : Window
    {
        public Entities db;
      
        public RoomWindow(ref Entities db1) //initilize the data

        {
            Cursor = Cursors.Wait;
            db = db1;
           
            InitializeComponent();

            //fills the list with rooms
            var rooms2 = from p in db.Rooms

                         select new { p.RoomNumber, p.RoomName};
//            var roomList = from r in db.Rooms select r.RoomName;
            // Display the PO number in the combo box.
            dgRooms.ItemsSource = rooms2.ToList();
            dgRooms.SelectedValuePath = "RoomNumber";
            dgRooms.SelectedIndex = 0;
            Cursor = Cursors.Arrow;

            // Bind the combo box to the ObjectResult of SalesOrderHeader 
            // that is returned when the query is executed.
            //this.ordersListBox.DataSource = orderQuery.Execute(MergeOption.AppendOnly);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) //creates a room update window
        {
            int i = (int)dgRooms.SelectedValue;
             Room thisRoom = db.Rooms.Single(r => r.RoomNumber == (int)dgRooms.SelectedValue);
                UpdateRoom frmUpdate = new UpdateRoom(ref db, ref thisRoom);
                frmUpdate.ShowDialog();


            //refreshes the list
                var rooms2 = from p in db.Rooms

                             select new { p.RoomNumber, p.RoomName };
                //            var roomList = from r in db.Rooms select r.RoomName;
                // Display the PO number in the combo box.

           
                dgRooms.ItemsSource = rooms2.ToList();
                dgRooms.SelectedValuePath = "RoomNumber";
                dgRooms.SelectedIndex = 0;

            
        }

        private void Button_Click(object sender, RoutedEventArgs e) //deletes a room
        {

            //checks to make sure the room isn't in an appointment
            var countofapps = (from c in db.Appointments where c.RoomNumber == (int)dgRooms.SelectedValue select c).Count();
            //var sqlCnt = "SELECT count(*) FROM appointment where RoomID = " + txtBoxID.Text;
            //var queryResult = _context.Database.SqlQuery<int>(sqlCnt).Single();

            if (countofapps > 0)
            {
                MessageBox.Show("Due to HIPPA regulations, this record cannot be deleted.");
            }
            else
            {
                Room thisRoom = db.Rooms.Single(c1 => c1.RoomNumber == (int)dgRooms.SelectedValue);

                db.Rooms.Remove(thisRoom);
                //db.Database.ExecuteSqlCommand("Delete from Room Where RoomID =" + txtBoxID.Text);
                //db.Database.ExecuteSqlCommand("Delete from Person Where PersonID =" + txtBoxID.Text);
                db.SaveChanges();
                MessageBox.Show("Room was successfully deleted.");

                var rooms2 = from p in db.Rooms

                             select new { p.RoomNumber, p.RoomName };
                //            var roomList = from r in db.Rooms select r.RoomName;
                // Display the PO number in the combo box.
                dgRooms.ItemsSource = rooms2.ToList();
                dgRooms.SelectedValuePath = "RoomNumber";
                dgRooms.SelectedIndex = 0;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e) //creates a new room
        {
            try
            {
                Room room = new Room();
                try
                {
                    room.RoomNumber = int.Parse(RNumberBox.Text);
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Room Number must be an integer");
                    throw new System.FormatException();
                }

                //checks to make sure the room doesn't already exist
                var rooms = db.Rooms.Count(a => a.RoomNumber == room.RoomNumber);
                if (rooms > 0)
                {
                    MessageBox.Show("Room number must not exist");
                }
                else
                {

                    //clears the items so it can be used again
                    room.RoomName = RNameBox.Text;
                    db.Rooms.Add(room);
                    db.SaveChanges();
                    RNameBox.Clear();
                    RNumberBox.Clear();

                    //refreshes the list
                    var rooms2 = from p in db.Rooms

                                 select new { p.RoomNumber, p.RoomName };
                    //            var roomList = from r in db.Rooms select r.RoomName;
                    // Display the PO number in the combo box.
                    dgRooms.ItemsSource = rooms2.ToList();
                    dgRooms.SelectedValuePath = "RoomNumber";
                    dgRooms.SelectedIndex = 0;
                    UpdateTab.IsSelected = true;
                    System.Windows.MessageBox.Show("Record Successfully Added");
                }
            }
            catch (System.FormatException)
            {

            }
            /*catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Do not enter a duplicate Room Number");
            }*/

        }

        private void RNameBox_KeyUp(object sender, KeyEventArgs e) //adds the room when the update button is clicked
        {
            if (e.Key == Key.Enter)
            {
                AddBtn_Click(sender, e);
            }
        }

    }
}
