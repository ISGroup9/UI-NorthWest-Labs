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
    /// Interaction logic for UpdateEmployee.xaml
    /// </summary>
    public partial class UpdateEmployee : Window
    {
        //establish the connection with the database
         public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //create a get and set
        public Employee _employee { get; set; }

        //load up the employee, initialize components, populate fields from page to table
        public UpdateEmployee(Employee employee)
        {
            _employee = employee;
            InitializeComponent();
            populateFields();
        }

        //populate the fields from the datagrid and put in the new window
        private void populateFields()
        {
            txtBoxFName.Text = _employee.Person.FirstName;
            txtBoxLName.Text = _employee.Person.LastName;
            txtBoxCity.Text = _employee.Person.City;
            cbState.Text = _employee.Person.State;
            txtBoxZip.Text = _employee.Person.ZIP;
            txtBoxPhone.Text = _employee.Person.Phone;
            txtBoxEmail.Text = _employee.Person.Email;
            txtBoxHireDate.Text = Convert.ToString(_employee.DateHired);
            txtBoxWage.Text = Convert.ToString(_employee.Wage);
        }

        //when the update button is pressed, update the database
        private void btnUpdateEmployee_MouseDown(object sender, RoutedEventArgs e)
        {
            _employee.Person.FirstName = txtBoxFName.Text;
            _employee.Person.LastName = txtBoxLName.Text;
            _employee.Person.City = txtBoxCity.Text;
            _employee.Person.State = cbState.Text;
            _employee.Person.ZIP = txtBoxZip.Text;
            _employee.Person.Phone = txtBoxPhone.Text;
            _employee.Person.Email = txtBoxEmail.Text;
            _employee.DateHired = Convert.ToDateTime(txtBoxHireDate.Text);
            _employee.Wage = Convert.ToDecimal(txtBoxWage.Text);
            _context.SaveChanges();

            //update the datagrid
            ((EmployeePage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

            this.Close();

            MessageBox.Show("Employee was successfully updated.");
                
        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}
