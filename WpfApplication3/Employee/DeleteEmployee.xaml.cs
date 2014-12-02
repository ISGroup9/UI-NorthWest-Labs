using System;
using System.Collections.Generic;
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
    /// Interaction logic for DeleteEmployee.xaml
    /// </summary>
    public partial class DeleteEmployee : Window
    {
        //create the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //create get and set
        public Employee _employee { get; set; }

        //initialize componenets, populate field from page to new window.
        public DeleteEmployee(Employee employee)
        {
            _employee = employee;
            InitializeComponent();
            populateFields();
        }

        //populate the fields from the page to the window
        private void populateFields()
        {
            txtBoxEmployeeID.Text = Convert.ToString(_employee.Person.Employee.EmployeeID);
            txtBoxFName.Text = _employee.Person.FirstName;
            txtBoxLName.Text = _employee.Person.LastName;
            txtBoxPhone.Text = _employee.Person.Phone;
        }

        //when the delete button is pressed, delete
        private void btnDeleteEmployee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            //get the number of employees in the appointment table
            var sqlCnt = "SELECT count(*) FROM appointment where EmployeeID = " + txtBoxEmployeeID.Text;
            var queryResult = _context.Database.SqlQuery<int>(sqlCnt).Single();

            if (queryResult > 0)
            {
                //display message to the user
                MessageBox.Show("Warning:  Due to HIPPA regulations, this employee cannot be deleted.");
            }
            else
            {
                //#1 - delete employee from the employee table 
                 _context.Database.ExecuteSqlCommand("Delete from Employee where EmployeeID =  " + txtBoxEmployeeID.Text);

                //#2 - delete employee from the person table 
                 _context.Database.ExecuteSqlCommand("Delete from Person where PersonID =  " + txtBoxEmployeeID.Text);
                
                //reload the window to update the grid
                ((EmployeePage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();
                
                //close the window
                this.Close();
                MessageBox.Show("Employee was successfully deleted.");
            }
        }


        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
