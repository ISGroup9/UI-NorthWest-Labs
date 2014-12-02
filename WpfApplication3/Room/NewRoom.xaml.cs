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
    /// <summary>
    /// Interaction logic for NewRoom.xaml
    /// </summary>
    public partial class NewRoom : Window
    {
        //set up the database connection
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //set up get and set 
        public Room _Room { get; set; }

        //load up the components
        public NewRoom(Room Room)
        {
            _Room = Room;
            InitializeComponent();
        }

        //when the create button is pressed, insert data from window into database
        private void btnCreateRoom_MouseDown(object sender, RoutedEventArgs e)
        {
            _context.Database.ExecuteSqlCommand("insert into room(roomnumber, roomName) values(@RoomNumber,@RoomName);",
                new SqlParameter("RoomNumber", txtBoxRoomNumber.Text),
                new SqlParameter("RoomName", txtBoxRoomName.Text));


            ((RoomPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

            this.Close();

            MessageBox.Show("Room was successfully added.");
        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}


//    public partial class MainWindow : Window
//    {
//        NorthwindEntities context = new NorthwindEntities();
//        bool isInsertMode = false;
//        bool isBeingEdited = false;

//        public MainWindow()
//        {
//            InitializeComponent();
//        }
//        private void Window_Loaded(object sender, RoutedEventArgs e)
//        {
//            dgEmp.ItemsSource = GetRoomList();
//        }
//        private ObservableCollection<Room> GetRoomList()
//        {
//            var list = from e in context.Rooms select e;
//            return new ObservableCollection<Room>(list);
//        }

//        private void dgEmp_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
//        {
//            Room Room = new Room();
//            Room emp = e.Row.DataContext as Room;
//            if (isInsertMode)
//            {
//                var InsertRecord = MessageBox.Show("Do you want to add " + emp.FirstName + " as a new emploee?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
//                if (InsertRecord == MessageBoxResult.Yes)
//                {
//                    Room.FirstName = emp.FirstName;
//                    Room.LastName = emp.LastName;
//                    Room.Title = emp.Title;
//                    Room.BirthDate = emp.BirthDate;
//                    context.Rooms.Add(Room);
//                    context.SaveChanges();
//                    dgEmp.ItemsSource = GetRoomList();
//                    MessageBox.Show(Room.FirstName + " " + Room.LastName + " has being added!", "Inserting Record", MessageBoxButton.OK, MessageBoxImage.Information);
//                }
//                else
//                    dgEmp.ItemsSource = GetRoomList();
//            }
//            context.SaveChanges();
//        }

//        private void dgEmp_PreviewKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.Key == Key.Delete && !isBeingEdited)
//            {
//                var grid = (DataGrid)sender;
//                if (grid.SelectedItems.Count > 0)
//                {
//                    var Res = MessageBox.Show("Are you sure you want to delete " + grid.SelectedItems.Count + " Rooms?", "Deleting Records", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
//                    if (Res == MessageBoxResult.Yes)
//                    {
//                        foreach (var row in grid.SelectedItems)
//                        {
//                            Room Room = row as Room;
//                            context.Rooms.Remove(Room);
//                        }
//                        context.SaveChanges();
//                        MessageBox.Show(grid.SelectedItems.Count + " Rooms have being deleted!");
//                    }
//                    else
//                        dgEmp.ItemsSource = GetRoomList();
//                }
//            }
//        }

//        private void dgEmp_AddingNewItem(object sender, AddingNewItemEventArgs e)
//        {
//            isInsertMode = true;
//        }

//        private void dgEmp_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
//        {
//            isBeingEdited = true;
//        }
//    }
//}