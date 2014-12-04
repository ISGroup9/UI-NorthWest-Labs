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

namespace UI_NorthWest_Labs2.Pages.Pricing.UpdatePricing
{

    public partial class UpdatePricing : UserControl
    {
        public UpdatePricing()
        {
            Random random = new Random();
            string randomdollar = "$" + string.Format("{0:0.##}", new Random().NextDouble() * 10);
            
            InitializeComponent();

            UpdateField.Text = randomdollar;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationCommands.BrowseBack.Execute(null, MainWindow.frame);
            string pricechange = UpdateField.Text;
            UpdateConfirmation uc = new UpdateConfirmation(pricechange);
            uc.ShowDialog();
        }
    }
}
