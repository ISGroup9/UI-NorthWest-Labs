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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UpdateRoom : Window
    {
        public Entities db;
        private Room room;

        public UpdateRoom(ref Entities db1,ref Room room1)//creates the window from the data from the room passed
        {
            Cursor = Cursors.Wait;
            db = db1;
            room = room1;
            InitializeComponent();
            Cursor = Cursors.Arrow;

            RNameBox.Text = room.RoomName;
            RNumberBox.Text = room.RoomNumber.ToString();
        }

        private void Update2Button_Click(object sender, RoutedEventArgs e) //updates the room from the name provided
        {
            
            room.RoomName = RNameBox.Text;
            
                

            db.SaveChanges();
            
            this.Close();
             
        }

        private void closebtn_Click(object sender, RoutedEventArgs e) //closes the window
        {
            this.Close();
        }

        private void RNameBox_KeyUp(object sender, KeyEventArgs e) //updates the room on enter key press
        {
            if (e.Key == Key.Enter)
            {
                Update2Button_Click(sender, e);
            }
        }
    }
}
