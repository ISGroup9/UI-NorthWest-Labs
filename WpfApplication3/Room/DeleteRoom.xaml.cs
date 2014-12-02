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
    /// Interaction logic for DeleteRoom.xaml
    /// </summary>
    public partial class DeleteRoom : Window
    {
        //create the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //create get and set
        public Room _Room { get; set; }

        //initialize componenets, populate field from page to new window.
        public DeleteRoom(Room Room)
        {
            _Room = Room;
            InitializeComponent();
            populateFields();
        }


        //populate the fields from the page to the window
        private void populateFields()
        {
            txtBoxRoomID.Text = Convert.ToString(_Room.RoomNumber);
            txtBoxRoomNumber.Text = Convert.ToString(_Room.RoomNumber);
            txtBoxRoomName.Text = _Room.RoomName;

        }

        //when the delete button is pressed, delete
        private void btnDeleteRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //get the number of Rooms in the appointment table
            var sqlCnt = "SELECT count(*) FROM appointment where RoomNumber = " + txtBoxRoomID.Text;
            var queryResult = _context.Database.SqlQuery<int>(sqlCnt).Single();

            if (queryResult > 0)
            {
                //display message to the user
                MessageBox.Show("Warning:  Due to HIPPA regulations, this Room cannot be deleted.");
            }
            else
            {
                //#1 - delete Room from the Room table 
                _context.Database.ExecuteSqlCommand("Delete from Room where RoomNumber =  " + txtBoxRoomID.Text);

                //reload the window to update the grid
                ((RoomPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

                //close the window
                this.Close();
                MessageBox.Show("Room was successfully deleted.");
            }

        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
