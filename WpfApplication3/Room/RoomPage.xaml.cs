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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for RoomPage.xaml
    /// </summary>
    public partial class RoomPage : Page
    {
        //set up the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //initialize the components, load up the datagrid
        public RoomPage()
        {
            InitializeComponent();
            InitializeData();
        }

        //get the table from the database and load it up into the datagrid
        public void InitializeData()
        {
            var emplist = _context.Room.Select(e => new EmpItem() { RoomID = e.RoomNumber, RoomNumber = e.RoomNumber, RoomName = e.RoomName }).ToList();
            dgRooms.ItemsSource = emplist;

            int index = 0;
            dgRooms.SelectedItem = dgRooms.Items[index];
            dgRooms.ScrollIntoView(dgRooms.Items[index]);
        }

        //when the update button is pressed, open a new window to update it
        private void btnUpdateRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var RoomTemp = (EmpItem)dgRooms.SelectedItem;
            var Room = _context.Room.Where(em => em.RoomNumber == RoomTemp.RoomID).FirstOrDefault<Room>();
            var newEditRoomWindow = new UpdateRoom(Room);
            newEditRoomWindow.ShowDialog();
        }

        //when the new button is pressed, open a new window to create it
        private void btnNewRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var RoomTemp = (EmpItem)dgRooms.SelectedItem;
            var Room = _context.Room.Where(em => em.RoomNumber == RoomTemp.RoomID).FirstOrDefault<Room>();
            var newEditRoomWindow = new NewRoom(Room);
            newEditRoomWindow.ShowDialog();
        }

        //whent he delete button is pressed, open a new window to delete it
        private void btnDeleteRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var RoomTemp = (EmpItem)dgRooms.SelectedItem;
            var Room = _context.Room.Where(em => em.RoomNumber == RoomTemp.RoomID).FirstOrDefault<Room>();
            var newEditRoomWindow = new DeleteRoom(Room);
            newEditRoomWindow.ShowDialog();
        }

        //getters and setters for class
        public class EmpItem
        {
            public EmpItem() { }
            public int RoomID { get; set; }
            public int RoomNumber { get; set; }
            public string RoomName { get; set; }

        }

    }
}
