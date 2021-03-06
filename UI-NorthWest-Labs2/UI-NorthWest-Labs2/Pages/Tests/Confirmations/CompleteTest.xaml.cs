﻿using FirstFloor.ModernUI.Windows.Controls;
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

namespace UI_NorthWest_Labs2.Pages.Tests.Confirmations
{
    /// <summary>
    /// Interaction logic for CompleteTest.xaml
    /// </summary>
    public partial class CompleteTest : ModernDialog
    {
        public CompleteTest()
        {
            InitializeComponent();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }
    }
}
