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
    /// Interaction logic for UpdateRoom.xaml
    /// </summary>
    public partial class UpdateRoom : Window
    {
        //establish the connection with the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //create a get and set
        public Room _Room { get; set; }

        //load up the Room, initialize components, populate fields from page to table
        public UpdateRoom(Room Room)
        {
            _Room = Room;
            InitializeComponent();
            populateFields();
        }

        //populate the fields from the datagrid and put in the new window
        private void populateFields()
        {
            txtBoxRoomNumber.Text = Convert.ToString(_Room.RoomNumber);
            txtBoxRoomName.Text = _Room.RoomName;
        }

        //when the update button is pressed, update the database
        private void btnUpdateRoom_MouseDown(object sender, RoutedEventArgs e)
        {
            _Room.RoomNumber = Convert.ToInt16(txtBoxRoomNumber.Text);
            _Room.RoomName = txtBoxRoomName.Text;

            //update the database
            _context.SaveChanges();

            ((RoomPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

            //close the window
            this.Close();

            MessageBox.Show("Room was successfully updated.");

        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}
