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
    public partial class UpdateEmployee : Window
    {
        
        public Entities db;

       
        private PersonType type1;
        private Employee emp1;
        public UpdateEmployee(PersonType type, ref Employee emp, ref Entities db1)//creates the window from the data from the employee given
        {
            Cursor = Cursors.Wait;
            type1 = type;
            emp1 = emp;
            db = db1;
            
            InitializeComponent();
            this.Tag = "Employee";
            DynamicTextBox1.Tag = "Wage";
            // SelectFrame.Tag = "Employee";

            
            HireDateSelector.SelectedDate = emp.DateHired;

            FNameBox.Text = emp1.Person.FirstName;
            LNameBox.Text = emp1.Person.LastName;
            CityBox.Text = emp1.Person.City;
            StateBox.SelectedItem = emp1.Person.State;
            ZipBox.Text = emp1.Person.ZIP;
            PhoneBox.Text = emp1.Person.Phone;
            EmailBox.Text = emp1.Person.Email;
            DefaultTextField1.Text = emp1.Wage.ToString();
            int i;
            
            for (i = 0; i < StateBox.Items.Count+1; i++)
            {
                StateBox.SelectedIndex = i;
                if (StateBox.SelectionBoxItem.ToString() == emp1.Person.State)
                {
                    break;
                }
            }
            if (i > StateBox.Items.Count)
            {
                StateBox.SelectedIndex = -1;
            }

            var emplist1 = from e in db.Employees orderby e.Person.LastName select new { e.EmployeeID, e.Person.LastName, Name = e.Person.LastName + ", " + e.Person.FirstName };
            var emplist = emplist1.ToList();
            ReferredByBox1.ItemsSource = emplist;
            ReferredByBox1.DisplayMemberPath = "Name";
            ReferredByBox1.SelectedValuePath = "EmployeeID";
            ReferredByBox1.SelectedValue = emp.SupervisorID;
            Cursor = Cursors.Arrow;
            //Email



        }

        

     

        
        private void Update1Button_Click(object sender, RoutedEventArgs e) //updates the employee from the boxes
        {
            
            try
            {
                
                    emp1.Person.FirstName = FNameBox.Text;
                    emp1.Person.LastName = LNameBox.Text;
                    emp1.Person.City = CityBox.Text;
                    
                        emp1.Person.State = (string)StateBox.SelectionBoxItem;
                    
                    
                    try
                    {
                    emp1.Person.ZIP = int.Parse(ZipBox.Text).ToString();
                    }
                    catch
                    {
                        MessageBox.Show("ZIP Codes must only contain digits");
                        throw new System.FormatException();
                    }
                    emp1.Person.Phone = PhoneBox.Text;
                    emp1.Person.Email = EmailBox.Text;
                    
                    try
                    {
                        emp1.Wage = decimal.Parse(DefaultTextField1.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Wages must be a decimal number");
                        throw new System.FormatException();
                    }
                    emp1.DateHired = HireDateSelector.SelectedDate;
                    emp1.Person.State = StateBox.SelectionBoxItem.ToString();
                    emp1.SupervisorID = (int)ReferredByBox1.SelectedValue;


                db.SaveChanges();
                this.Close();
            }
            catch (System.FormatException) {  };
        }

        

        private void CloseButton_Click(object sender, RoutedEventArgs e) //closes the window
        {
            this.Close();
        }
    }
}
