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

namespace UI_NorthWest_Labs2.Pages.LoginDialog
{
    /// <summary>
    /// Interaction logic for ModernDialog1.xaml
    /// </summary>
    public partial class LoginDialog : ModernDialog
    {
        public LoginDialog()
        {
            InitializeComponent();

            // define the dialog buttons
           // Button button = new Button();
            this.OkButton.Content = "Login";
            //button.Content = "Login";
            this.Buttons = new Button[] { this.OkButton };
            //this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }
    }
}
