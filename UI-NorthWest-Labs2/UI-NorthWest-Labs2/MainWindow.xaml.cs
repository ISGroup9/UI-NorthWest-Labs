using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
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
using UI_NorthWest_Labs2.Pages.LoginDialog;

namespace UI_NorthWest_Labs2
{
    /// <summary>
    /// Updated Dec. 5, 2014
    /// The following is a UI design for Northwest Labs LLC. This software will allow the user to 
    /// maintain customer, work order, sales, pricing, and catalog information
    /// The system will work with the server's active directory for employee login information
    /// The system can be used by workers, management, sales representative, and finance for various use cases.
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public static IInputElement frame;
        public static Uri uri = new Uri("/Assets/ModernUi.OurTheme.xaml", UriKind.Relative);
        public MainWindow()

        {
            
            AppearanceManager.Current.ThemeSource = uri;
            
            InitializeComponent();
            
            
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            frame = (ModernFrame)GetTemplateChild("ContentFrame");
        }

        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoginDialog l = new LoginDialog();
            l.ShowDialog();
        }
    }

}
