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
using UI_NorthWest_Labs2.Pages.WorkOrders.Confirmations;

namespace UI_NorthWest_Labs2.Pages.WorkOrders
{
    /// <summary>
    /// Interaction logic for AddOptionalInfo.xaml
    /// </summary>
    public partial class AddOptionalInfo : UserControl
    {
        public AddOptionalInfo()
        {
            InitializeComponent();
        }

        private void BeginBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationCommands.GoToPage.Execute("/Pages/WorkOrders/InProcess/WorkOrder1.xaml", this);
            WorkOrderUpdatedDialog d = new WorkOrderUpdatedDialog();
            d.ShowDialog();
        }
    }
}
