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
            MessageBox.Show("I hate when you guys make fun of me");
        }

        private void DownloadProtocol_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("I hate when you guys make fun of me");
        }

    }
}
