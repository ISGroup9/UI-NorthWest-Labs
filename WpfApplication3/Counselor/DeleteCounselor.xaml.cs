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
    /// Interaction logic for DeleteCounselor.xaml
    /// </summary>
    public partial class DeleteCounselor : Window
    {
        //create the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //create get and set
        public Counselor _Counselor { get; set; }

        //initialize componenets, populate field from page to new window.
        public DeleteCounselor(Counselor Counselor)
        {
            _Counselor = Counselor;
            InitializeComponent();
            populateFields();
        }

        //populate the fields from the page to the window
        private void populateFields()
        {
            txtBoxCounselorID.Text = Convert.ToString(_Counselor.Person.Counselor.CounselorID);
            txtBoxFName.Text = _Counselor.Person.FirstName;
            txtBoxLName.Text = _Counselor.Person.LastName;
            txtBoxPhone.Text = _Counselor.Person.Phone;
        }

        //when the delete button is pressed, delete
        private void btnDeleteCounselor_MouseDown(object sender, MouseButtonEventArgs e)
        {

            //get the number of Counselors in the appointment table
            var sqlCnt = "SELECT count(*) FROM appointment where CounselorID = " + txtBoxCounselorID.Text;
            var queryResult = _context.Database.SqlQuery<int>(sqlCnt).Single();

            if (queryResult > 0)
            {
                //display message to the user
                MessageBox.Show("Warning:  Due to HIPPA regulations, this Counselor cannot be deleted.");
            }
            else
            {
                //#1 - delete Counselor from the Counselor table 
                _context.Database.ExecuteSqlCommand("Delete from Counselor where CounselorID = " + txtBoxCounselorID.Text);

                //#2 - delete Counselor from the person table 
                _context.Database.ExecuteSqlCommand("Delete from Person where PersonID =  " + txtBoxCounselorID.Text);
                

                //reload the window to update the grid
                ((CounselorPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

                //close the window
                this.Close();

                MessageBox.Show("Counselor was successfully deleted.");
            }
        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
