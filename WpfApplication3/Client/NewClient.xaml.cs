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
    /// Interaction logic for NewClient.xaml
    /// </summary>
    public partial class NewClient : Window
    {
        //set up the database connection
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //set up get and set 
        public Client _Client { get; set; }

        //load up the components
        public NewClient(Client Client)
        {
            _Client = Client;
            InitializeComponent();

            //load up the referredby combobox
            var clientlist = from c in _context.Client orderby c.Person.LastName select new { c.ClientID, c.Person.LastName, Name = c.Person.LastName + ", " + c.Person.FirstName };
            cbReferredBy.ItemsSource = clientlist.ToList();
            cbReferredBy.DisplayMemberPath = "Name";
            cbReferredBy.SelectedValuePath = "ClientID";
        }

        //when the create button is pressed, insert data from window into database
        private void btnCreateClient_MouseDown(object sender, RoutedEventArgs e)
        {
            _context.Database.ExecuteSqlCommand("Insert Into Person(firstname,lastname,city,state,zip,phone,email) values (@firstname,@lastname,@city,@state,@zip,@phone,@email);",
                new SqlParameter("firstname", txtBoxFName.Text),
                new SqlParameter("LastName", txtBoxLName.Text),
                new SqlParameter("City", txtBoxCity.Text),
                new SqlParameter("State", cbState.Text),
                new SqlParameter("ZIP", txtBoxZip.Text),
                new SqlParameter("Phone", txtBoxPhone.Text),
                new SqlParameter("Email", txtBoxEmail.Text));


            _context.Database.ExecuteSqlCommand("Insert into Client values((select max(personid) from person), @FoundOut, @ReminderMethod, @ReferredBy);",
                new SqlParameter("FoundOut", txtBoxFoundOut.Text),
                new SqlParameter("ReminderMethod", txtBoxReminderMethod.Text),
                new SqlParameter("ReferredBy", cbReferredBy.SelectedValue));

            ((ClientPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

            this.Close();

            MessageBox.Show("Client was successfully added.");
        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}

