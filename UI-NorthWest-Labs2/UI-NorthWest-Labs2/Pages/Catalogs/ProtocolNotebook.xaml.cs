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
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowDialog();

            //MessageBox("File Uploaded Successfully");
        }

        private void DownloadProtocol_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

            saveFileDialog1.Filter = "PDF files (*.PDF)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
        }

    }
}
