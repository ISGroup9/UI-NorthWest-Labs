//*********************************************************************************************
//* Taylor Curtis
//* November 15, 2014
//*
//* Description:
//* The following program is a scheudling tool for New Dawn Family Counseling.  Users Can
//* add, update, delete, and view information about the following: Appointments, Clients,
//* Room, Counselors, and Employees.  The user can navigate between screens by using the
//* menu bar at the top of the main menu screen.  If you are on a specific page, you can 
//* naviagte to other windows to add, update, and delete records shown in the table.  The 
//* user can select a row in the table and then select the button to perform the action.
//* *********************************************************************************************




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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    

    public partial class MainWindow : Window
    {

        public NDFCEntities dbContext { get; private set; }


        public MainWindow()
        {
            dbContext = new NDFCEntities();
            InitializeComponent();
            frame.Content = new MainPage();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = null;
            frame.Content = new EmployeePage();
        }

        private void Counselor_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = null;
            frame.Content = new CounselorPage();
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = null;
            frame.Content = new RoomPage();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = null;
            frame.Content = new ClientPage();
        }

        private void Appt_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = null;
            frame.Content = new AppointmentPage();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = null;
            frame.Content = new AboutPage();
        }


    }
}
