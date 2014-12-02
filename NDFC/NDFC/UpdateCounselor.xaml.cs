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
    /// Interaction logic for Update.xaml
    /// This window is so that you can view/update the info for a single record in the system
    /// </summary>
    public partial class UpdateCounselor : Window
    {
        
        public Entities db;

       
        private Counselor counselor;
        public UpdateCounselor(ref Counselor couns1, ref Entities db1) //creates the window, set the data
        {
            Cursor = Cursors.Wait;
            
            counselor = couns1;
            db = db1;
            
            InitializeComponent();
            
            

            FNameBox.Text = counselor.Person.FirstName;
            LNameBox.Text = counselor.Person.LastName;
            CityBox.Text = counselor.Person.City;
            StateBox.SelectedItem = counselor.Person.State;
            ZipBox.Text = counselor.Person.ZIP;
            PhoneBox.Text = counselor.Person.Phone;
            EmailBox.Text = counselor.Person.Email;
            DegreeSuffixBox.Text = counselor.DegreeSuffix;
            int i;
            for (i = 0; i < StateBox.Items.Count+1; i++)
            {
                StateBox.SelectedIndex = i;
                if (StateBox.SelectionBoxItem.ToString() == counselor.Person.State)
                {
                    break;
                }
            }

            if (i > StateBox.Items.Count)
            {
                StateBox.SelectedIndex = -1;
            }
            
            //Email
            Cursor = Cursors.Arrow;
            
        }

        


        private void Update1Button_Click(object sender, RoutedEventArgs e) //updates the counselor from the data in the boxes
        {
            try
            {

                    counselor.Person.FirstName = FNameBox.Text;
                    
                    counselor.Person.LastName = LNameBox.Text;
                    
                    counselor.Person.City = CityBox.Text;
                  
                    counselor.Person.State = (string)StateBox.SelectionBoxItem;

                    try
                    {
                        counselor.Person.ZIP = int.Parse(ZipBox.Text).ToString();
                    }
                    catch
                    {
                        MessageBox.Show("ZIP Codes must only contain digits");
                        throw new System.FormatException();
                    }
                    
                    counselor.Person.Phone = PhoneBox.Text;
                   
                    counselor.Person.Email = EmailBox.Text;
                    
                    counselor.DegreeSuffix = DegreeSuffixBox.Text;
                 
                    
                    
                    

                


                db.SaveChanges();
                this.Close();
            }
            catch (System.FormatException) { MessageBox.Show("Incorrect Format"); }
        }

        

        private void CloseButton_Click(object sender, RoutedEventArgs e) //closes the window
        {
            this.Close();
        }
    }
}
