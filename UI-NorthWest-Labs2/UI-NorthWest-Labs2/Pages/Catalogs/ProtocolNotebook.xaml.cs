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

namespace UI_NorthWest_Labs2.Pages.Catalogs
{
    /// <summary>
    /// Interaction logic for ProtocolNotebook.xaml
    /// </summary>
    public partial class ProtocolNotebook : UserControl
    {
        public ProtocolNotebook()
        {
            InitializeComponent();
        }

        private void UploadProtocol_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "PDF files (*.PDF)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            string filepath = AppDomain.CurrentDomain.BaseDirectory;
            filepath = filepath.Remove(filepath.Length - 10);
            openFileDialog1.InitialDirectory = filepath + "PDF";
            openFileDialog1.ShowDialog();

            MessageBox.Show("File Uploaded Successfully");
        }

        private void DownloadProtocol_Click(object sender, RoutedEventArgs e)
        {
            //set pdf name here
            string pdfname = "FinishedProtocol";

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
