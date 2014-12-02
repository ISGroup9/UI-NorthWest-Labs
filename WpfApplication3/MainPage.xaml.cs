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

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for CounselorPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public NDFCEntities _context = ((MainWindow)Application.Current.MainWindow).dbContext;
        public MainPage()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            var sb = new StringBuilder();
            sb.Append("Select a.appointmentID as ApptID, s.clientID, client.FirstName + ' ' + client.Lastname AS ClientName,  a.StartTime AS AppDate, counselor.FirstName + ' ' + counselor.LastName AS CounselorName, a.RoomNumber, r.RoomName,employee.FirstName + ' ' + employee.LastName AS EmployeeName");
            sb.Append(" FROM Appointment a");
            sb.Append(" LEFT JOIN ScheduledFor s on s.AppointmentID = a.AppointmentID");
            sb.Append(" LEFT JOIN Person client ON client.personID = s.ClientID");
            sb.Append(" LEFT JOIN Person counselor ON counselor.personID = a.CounselorID");
            sb.Append(" LEFT JOIN Person employee ON employee.personID = a.EmployeeID");
            sb.Append(" JOIN Room r on a.roomNumber = r.roomNumber");
            sb.Append(" order by a.StartTime asc");

            string sql = sb.ToString();

            var appointments = _context.Database.SqlQuery<EmpItem>(sql).ToList();
            dgAppointments.ItemsSource = appointments;
        }
    }

    public class EmpItem
{
        public int ApptID { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public DateTime? AppDate { get; set; }
        public string CounselorName { get; set; }
        public int RoomNumber { get; set; }
        public string RoomName { get; set; }
        public string EmployeeName { get; set; }
}

}