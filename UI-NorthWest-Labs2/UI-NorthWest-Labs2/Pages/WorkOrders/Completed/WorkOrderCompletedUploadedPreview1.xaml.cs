using Microsoft.Win32;
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
using UI_NorthWest_Labs2.Pages.Catalogs.Confirmation;

namespace UI_NorthWest_Labs2.Pages.WorkOrders.Completed
{
    /// <summary>
    /// Interaction logic for WorkOrderPreview.xaml
    /// </summary>
    public partial class WorkOrderCompletedUploadedPreview1 : UserControl
    {
        public WorkOrderCompletedUploadedPreview1()
        {
            InitializeComponent();
        }

        private void View_Work_Order_Click(object sender, RoutedEventArgs e)
        {
            ////set pdf name here
            //string pdfname = "SummaryReport";

            ////add PDF folder and .pdf to filename
            //string fullname = "\\PDF\\" + pdfname + ".pdf";

            ////find the full filepath
            //string filepath = AppDomain.CurrentDomain.BaseDirectory;

            ////remove 'bin' and 'debug' folder
            //filepath = filepath.Remove(filepath.Length - 10);

            ////open the pdf :)
            //System.Diagnostics.Process.Start(filepath + fullname);

            NavigationCommands.GoToPage.Execute("/Pages/WorkOrders/Completed/WorkOrder1Uploaded.xaml", MainWindow.frame);
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "PDF files (*.PDF)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            string filepath = AppDomain.CurrentDomain.BaseDirectory;
            filepath = filepath.Remove(filepath.Length - 10);
            openFileDialog1.InitialDirectory = filepath + "PDF";
            openFileDialog1.ShowDialog();

            FileUploaded f = new FileUploaded();
            f.ShowDialog();
        }
  
    }
}
