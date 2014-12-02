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
    /// Interaction logic for UpdateCounselor.xaml
    /// </summary>
    public partial class UpdateCounselor : Window
    {
        //establish the connection with the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //create a get and set
        public Counselor _Counselor { get; set; }

        //load up the Counselor, initialize components, populate fields from page to table
        public UpdateCounselor(Counselor Counselor)
        {
            _Counselor = Counselor;
            InitializeComponent();
            populateFields();
        }

        //populate the fields from the datagrid and put in the new window
        private void populateFields()
        {
            txtBoxFName.Text = _Counselor.Person.FirstName;
            txtBoxLName.Text = _Counselor.Person.LastName;
            txtBoxCity.Text = _Counselor.Person.City;
            cbState.Text = _Counselor.Person.State;
            txtBoxZip.Text = _Counselor.Person.ZIP;
            txtBoxPhone.Text = _Counselor.Person.Phone;
            txtBoxEmail.Text = _Counselor.Person.Email;
            txtBoxDegree.Text = _Counselor.DegreeSuffix;
        }

        //when the update button is pressed, update the database
        private void btnUpdateCounselor_MouseDown(object sender, RoutedEventArgs e)
        {
            _Counselor.Person.FirstName = txtBoxFName.Text;
            _Counselor.Person.LastName = txtBoxLName.Text;
            _Counselor.Person.City = txtBoxCity.Text;
            _Counselor.Person.State = cbState.Text;
            _Counselor.Person.ZIP = txtBoxZip.Text;
            _Counselor.Person.Phone = txtBoxPhone.Text;
            _Counselor.Person.Email = txtBoxEmail.Text;
            _Counselor.DegreeSuffix = txtBoxDegree.Text;
            _context.SaveChanges();

            ((CounselorPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

            this.Close();

            MessageBox.Show("Counselor was successfully updated.");

        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}
