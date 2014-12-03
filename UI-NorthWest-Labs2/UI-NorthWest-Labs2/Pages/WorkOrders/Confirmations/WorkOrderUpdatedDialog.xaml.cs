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

namespace UI_NorthWest_Labs2.Pages.WorkOrders.Confirmations
{
    /// <summary>
    /// Interaction logic for WorkOrderUpdatedDialog.xaml
    /// </summary>
    public partial class WorkOrderUpdatedDialog : ModernDialog
    {
        public WorkOrderUpdatedDialog()
        {
            InitializeComponent();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, };
        }
    }
}
