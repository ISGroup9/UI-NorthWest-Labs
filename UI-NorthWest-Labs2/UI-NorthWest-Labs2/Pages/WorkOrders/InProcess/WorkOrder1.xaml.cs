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

namespace UI_NorthWest_Labs2.Pages.WorkOrders.InProcess
{
    /// <summary>
    /// Interaction logic for ListPage1.xaml
    /// </summary>
    public partial class WorkOrder1 : UserControl
    {
        public WorkOrder1()
        {
            InitializeComponent();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            //set pdf name here
            string pdfname = "campusmap";

            //add PDF folder and .pdf to filename
            string fullname = "\\PDF\\" + pdfname + ".pdf";

            //find the full filepath
            string filepath = AppDomain.CurrentDomain.BaseDirectory;

            //remove 'bin' and 'debug' folder
            filepath = filepath.Remove(filepath.Length - 10);

            //open the pdf :)
            System.Diagnostics.Process.Start(filepath + fullname);
        }
    }
}
