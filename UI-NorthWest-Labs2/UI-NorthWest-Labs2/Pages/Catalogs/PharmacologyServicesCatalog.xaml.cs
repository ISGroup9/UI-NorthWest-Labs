using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PharmacologyServicesCatalog.xaml
    /// </summary>
    public partial class PharmacologyServicesCatalog : UserControl
    {
        public PharmacologyServicesCatalog()
        {
            InitializeComponent();
        }

        private void UploadCatalog_Click(object sender, RoutedEventArgs e)
        {
            
            //MessageBox.Show("I hate when you guys make fun of me");
        }

        private void DownloadCatalog_Click(object sender, RoutedEventArgs e)
        {
            
            SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

            saveFileDialog1.Filter = "PDF files (*.PDF)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();

            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    if ((myStream = saveFileDialog1.OpenFile()) != null)
            //    {
            //        // Code to write the stream goes here.
            //        myStream.Close();
            //    }
            //}
            //MessageBox.Show("I hate when you guys make fun of me");
        }
    }
}
