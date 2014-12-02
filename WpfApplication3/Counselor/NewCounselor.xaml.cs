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
    /// Interaction logic for NewCounselor.xaml
    /// </summary>
    public partial class NewCounselor : Window
    {
        //set up the database connection
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;

        //set up get and set 
        public Counselor _Counselor { get; set; }

        //load up the components
        public NewCounselor(Counselor Counselor)
        {
            _Counselor = Counselor;
            InitializeComponent();
        }

        //when the create button is pressed, insert data from window into database
        private void btnCreateCounselor_MouseDown(object sender, RoutedEventArgs e)
        {
            _context.Database.ExecuteSqlCommand("Insert Into Person(firstname,lastname,city,state,zip,phone,email) values (@firstname,@lastname,@city,@state,@zip,@phone,@email);",
                new SqlParameter("firstname", txtBoxFName.Text),
                new SqlParameter("LastName", txtBoxLName.Text),
                new SqlParameter("City", txtBoxCity.Text),
                new SqlParameter("State", cbState.Text),
                new SqlParameter("ZIP", txtBoxZip.Text),
                new SqlParameter("Phone", txtBoxPhone.Text),
                new SqlParameter("Email", txtBoxEmail.Text));


            _context.Database.ExecuteSqlCommand("insert into counselor values((select max(personid) from person),@DegreeSuffix);",
                new SqlParameter("DegreeSuffix", txtBoxDegree.Text));

            ((CounselorPage)(((MainWindow)Application.Current.MainWindow).frame.Content)).InitializeData();

            this.Close();

            MessageBox.Show("Counselor was successfully added.");
        }

        //when the cancel button is pressed, close the window
        private void btnCancelForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}


//    public partial class MainWindow : Window
//    {
//        NorthwindEntities context = new NorthwindEntities();
//        bool isInsertMode = false;
//        bool isBeingEdited = false;

//        public MainWindow()
//        {
//            InitializeComponent();
//        }
//        private void Window_Loaded(object sender, RoutedEventArgs e)
//        {
//            dgEmp.ItemsSource = GetCounselorList();
//        }
//        private ObservableCollection<Counselor> GetCounselorList()
//        {
//            var list = from e in context.Counselors select e;
//            return new ObservableCollection<Counselor>(list);
//        }

//        private void dgEmp_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
//        {
//            Counselor Counselor = new Counselor();
//            Counselor emp = e.Row.DataContext as Counselor;
//            if (isInsertMode)
//            {
//                var InsertRecord = MessageBox.Show("Do you want to add " + emp.FirstName + " as a new emploee?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
//                if (InsertRecord == MessageBoxResult.Yes)
//                {
//                    Counselor.FirstName = emp.FirstName;
//                    Counselor.LastName = emp.LastName;
//                    Counselor.Title = emp.Title;
//                    Counselor.BirthDate = emp.BirthDate;
//                    context.Counselors.Add(Counselor);
//                    context.SaveChanges();
//                    dgEmp.ItemsSource = GetCounselorList();
//                    MessageBox.Show(Counselor.FirstName + " " + Counselor.LastName + " has being added!", "Inserting Record", MessageBoxButton.OK, MessageBoxImage.Information);
//                }
//                else
//                    dgEmp.ItemsSource = GetCounselorList();
//            }
//            context.SaveChanges();
//        }

//        private void dgEmp_PreviewKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.Key == Key.Delete && !isBeingEdited)
//            {
//                var grid = (DataGrid)sender;
//                if (grid.SelectedItems.Count > 0)
//                {
//                    var Res = MessageBox.Show("Are you sure you want to delete " + grid.SelectedItems.Count + " Counselors?", "Deleting Records", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
//                    if (Res == MessageBoxResult.Yes)
//                    {
//                        foreach (var row in grid.SelectedItems)
//                        {
//                            Counselor Counselor = row as Counselor;
//                            context.Counselors.Remove(Counselor);
//                        }
//                        context.SaveChanges();
//                        MessageBox.Show(grid.SelectedItems.Count + " Counselors have being deleted!");
//                    }
//                    else
//                        dgEmp.ItemsSource = GetCounselorList();
//                }
//            }
//        }

//        private void dgEmp_AddingNewItem(object sender, AddingNewItemEventArgs e)
//        {
//            isInsertMode = true;
//        }

//        private void dgEmp_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
//        {
//            isBeingEdited = true;
//        }
//    }
//}