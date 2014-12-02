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
    /// Interaction logic for CounselorPage.xaml
    /// </summary>
    public partial class CounselorPage : Page
    {
        //set up the connection to the database
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //initialize the components, load up the datagrid
        public CounselorPage()
        {
            InitializeComponent();
            InitializeData();
        }

        //get the table from the database and load it up into the datagrid
        public void InitializeData()
        {
            var emplist = _context.Counselor.Select(e => new EmpItem() { CounselorID = e.CounselorID, First = e.Person.FirstName, Last = e.Person.LastName, City = e.Person.City, State = e.Person.State, ZIP = e.Person.ZIP, Phone = e.Person.Phone, Email = e.Person.Email, DegreeSuffix = e.DegreeSuffix }).ToList();
            dgCounselors.ItemsSource = emplist;

            int index = 0;
            dgCounselors.SelectedItem = dgCounselors.Items[index];
            dgCounselors.ScrollIntoView(dgCounselors.Items[index]);
        }

        //when the update button is pressed, open a new window to update it
        private void btnUpdateCounselor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var CounselorTemp = (EmpItem)dgCounselors.SelectedItem;
            var Counselor = _context.Counselor.Where(em => em.CounselorID == CounselorTemp.CounselorID).FirstOrDefault<Counselor>();
            var newEditCounselorWindow = new UpdateCounselor(Counselor);
            newEditCounselorWindow.ShowDialog();
        }

        //when the new button is pressed, open a new window to create it
        private void btnNewCounselor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var CounselorTemp = (EmpItem)dgCounselors.SelectedItem;
            var Counselor = _context.Counselor.Where(em => em.CounselorID == CounselorTemp.CounselorID).FirstOrDefault<Counselor>();
            var newEditCounselorWindow = new NewCounselor(Counselor);
            newEditCounselorWindow.ShowDialog();
        }

        //whent he delete button is pressed, open a new window to delete it
        private void btnDeleteCounselor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var CounselorTemp = (EmpItem)dgCounselors.SelectedItem;
            var Counselor = _context.Counselor.Where(em => em.CounselorID == CounselorTemp.CounselorID).FirstOrDefault<Counselor>();
            var newDeleteCounselorWindow = new DeleteCounselor(Counselor);
            newDeleteCounselorWindow.ShowDialog();
        }

        //getters and setters for class
        public class EmpItem
        {
            public EmpItem() { }
            public int CounselorID { get; set; }
            public string First { get; set; }
            public string Last { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZIP { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string DegreeSuffix { get; set; }

        }

    }
}
