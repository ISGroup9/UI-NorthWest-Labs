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
    /// Interaction logic for UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : Window
    {
        //establish the connection with the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //create a get and set
        public Client _Client { get; set; }

        //load up the Client, initialize components, populate fields from page to table
        public UpdateClient(Client Client)
        {
            _Client = Client;
            InitializeComponent();
            populateFields();
            //load up the referredby combobox
            var clientlist = from c in _context.Client orderby c.Person.LastName select new { c.ClientID, c.Person.LastName, Name = c.Person.LastName + ", " + c.Person.FirstName };
            cbReferredBy.ItemsSource = clientlist.ToList();
            cbReferredBy.DisplayMemberPath = "Name";
            cbReferredBy.SelectedValuePath = "ClientID";
        }

        //populate the fields from the datagrid and put in the new window
        private void populateFields()
        {
            txtBoxFName.Text = _Client.Person.FirstName;
            txtBoxLName.Text = _Client.Person.LastName;
            txtBoxCity.Text = _Client.Person.City;
            cbState.Text = _Client.Person.State;
            txtBoxZip.Text = _Client.Person.ZIP;
            txtBoxPhone.Text = _Client.Person.Phone;
            txtBoxEmail.Text = _Client.Person.Email;
            txtBoxFoundOut.Text = _Client.FoundOut;
            txtBoxReminderMethod.Text = _Client.ReminderMethod;
            cbReferredBy.SelectedValue = _Client.ReferredByClientID;
        }

        //when the update button is pressed, update the database
        private void btnUpdateClient_MouseDown(object sender, RoutedEventArgs e)
        {
            _Client.Person.FirstName = txtBoxFName.Text;
            _Client.Person.LastName = txtBoxLName.Text;
            _Client.Person.City = txtBoxCity.Text;
            _Client.Person.State = cbState.Text;
            _Client.Person.ZIP = txtBoxZip.Text;
            _Client.Person.Phone = txtBoxPhone.Text;
            _Client.Person.Email = txtBoxEmail.Text;
            _Client.FoundOut = txtBoxFoundOut.Text;
            _Client.ReminderMethod = txtBoxReminderMethod.Text;
            _Client.ReferredByClientID = Convert.ToInt16(cbReferredBy.SelectedValue);
            _context.SaveChanges();

            ((ClientPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

            this.Close();

            MessageBox.Show("Client was successfully updated.");

        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}
