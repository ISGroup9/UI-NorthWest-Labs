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

namespace UI_NorthWest_Labs2.Pages.Tests.Schedule
{
    /// <summary>
    /// Interaction logic for ViewSchedule.xaml
    /// </summary>
    public partial class ViewSchedule : UserControl
    {
        public ViewSchedule()
        {
            InitializeComponent();
        }

        private void Schedule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationCommands.GoToPage.Execute("/Pages/Tests/AllTestInfoScheduled/ScheduleTestInfoComplete1.xaml", MainWindow.frame);
        }
    }
}
