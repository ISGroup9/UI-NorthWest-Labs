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
    /// Interaction logic for ScheduleTests.xaml
    /// </summary>
    public partial class ScheduleTests : UserControl
    {
        public ScheduleTests()
        {
            InitializeComponent();
        }

        private void ScheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            ScheduleTestDialog d = new ScheduleTestDialog();
            d.ShowDialog();
            Schedule.Items.Remove(Schedule.SelectedItem);
        }

        private void Schedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScheduleBtn.IsEnabled = true;
        }

        private void Schedule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationCommands.GoToPage.Execute("/Pages/Tests/Received/ReceivedTestInfoComplete1.xaml", MainWindow.frame);
        }
    }
}
