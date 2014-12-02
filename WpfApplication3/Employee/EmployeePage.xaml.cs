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
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        //set up the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //initialize the components, load up the datagrid
        public EmployeePage()
        {
            InitializeComponent();
            InitializeData();
        }

        //get the table from the database and load it up into the datagrid
        public void InitializeData()
        {
            var emplist = _context.Employee.Select(e => new EmpItem() { EmployeeID = e.EmployeeID, First = e.Person.FirstName, Last = e.Person.LastName, City = e.Person.City, State = e.Person.State, ZIP = e.Person.ZIP, Phone = e.Person.Phone, Email = e.Person.Email, DateHired = e.DateHired, Wage = e.Wage }).ToList();
            dgEmployees.ItemsSource = emplist;

            int index = 0;
            dgEmployees.SelectedItem = dgEmployees.Items[index];
            dgEmployees.ScrollIntoView(dgEmployees.Items[index]);
        }

        //when the update button is pressed, open a new window to update it
        private void btnUpdateEmployee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var employeeTemp = (EmpItem)dgEmployees.SelectedItem;
            var employee = _context.Employee.Where(em => em.EmployeeID == employeeTemp.EmployeeID).FirstOrDefault<Employee>();
            var newEditEmployeeWindow = new UpdateEmployee(employee);
            newEditEmployeeWindow.ShowDialog();
        }

        //when the new button is pressed, open a new window to create it
        private void btnNewEmployee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var NewEmployee = new NewEmployee();
            NewEmployee.ShowDialog();
        }

        //whent he delete button is pressed, open a new window to delete it
        private void btnDeleteEmployee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var employeeTemp = (EmpItem)dgEmployees.SelectedItem;
            var employee = _context.Employee.Where(em => em.EmployeeID == employeeTemp.EmployeeID).FirstOrDefault<Employee>();
            var newDeleteEmployeeWindow = new DeleteEmployee(employee);
            newDeleteEmployeeWindow.ShowDialog();
        }

        //getters and setters for class
        public class EmpItem
        {
            public EmpItem() { }
            public int EmployeeID { get; set; }
            public string First { get; set; }
            public string Last { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZIP { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public DateTime? DateHired { get; set; }
            public decimal? Wage { get; set; }
        }



    }
}
