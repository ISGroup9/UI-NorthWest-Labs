using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace UI_NorthWest_Labs2.Pages.Sales
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class SaleConfirmation : ModernDialog
    {
        public SaleConfirmation()
        {
            InitializeComponent();
            this.Buttons = new Button[] { this.OkButton };
        }
    }
}