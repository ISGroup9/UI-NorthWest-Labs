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
using UI_NorthWest_Labs2.Pages.Billing;

namespace UI_NorthWest_Labs2.Pages
{
    /// <summary>
    /// Interaction logic for BillingPreview3.xaml
    /// </summary>
    public partial class BillingPreview3 : UserControl
    {
        public BillingPreview3()
        {
            InitializeComponent();
        }

        private void ApplyDiscountandBill_Click(object sender, RoutedEventArgs e)
        {
            BillingConfirmation newBill = new BillingConfirmation();
            newBill.ShowDialog();
        }
    }
}
