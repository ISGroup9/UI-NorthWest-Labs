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

namespace UI_NorthWest_Labs2.Pages.Sales.Reports
{
    /// <summary>
    /// Interaction logic for PerformanceReport.xaml
    /// </summary>
    public partial class PerformanceReport : UserControl
    {
        public PerformanceReport()
        {
            InitializeComponent();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            //set pdf name here
            string pdfname = "SalesReport";

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
