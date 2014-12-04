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
using UI_NorthWest_Labs2.Pages.Tests.Confirmations;

namespace UI_NorthWest_Labs2.Pages.Tests.AllTestInfoScheduled
{
    /// <summary>
    /// Interaction logic for TestInfo.xaml
    /// </summary>
    public partial class ScheduleTestInfoComplete1 : UserControl
    {
        public ScheduleTestInfoComplete1()
        {
            InitializeComponent();
        }

        private void Complete_Button_Click(object sender, RoutedEventArgs e)
        {
            CompleteTest c = new CompleteTest();
            c.ShowDialog();
            NavigationCommands.GoToPage.Execute("/Pages/Tests/CompletedTestInfo1.xaml", this);
        }
    }
}
