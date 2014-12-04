using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace UI_NorthWest_Labs2.Pages.Pricing
{
    /// <summary>
    /// Interaction logic for CreatePrices.xaml
    /// </summary>
    public partial class CreatePrices : UserControl
    {
        string saved;
        public CreatePrices()
        {
            InitializeComponent();
            
             saved = XamlWriter.Save(testgrid);
        }
        
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringReader sReader = new StringReader(saved);
            XmlReader xReader = XmlReader.Create(sReader);
            Grid testgrid1 = (Grid)XamlReader.Load(xReader);
            Tests.Children.Add(testgrid1);
            RemoveBtn.Visibility = System.Windows.Visibility.Visible;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            
            Tests.Children.RemoveAt(Tests.Children.Count-1);
            if (Tests.Children.Count <= 1)
            {
                RemoveBtn.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            double weightone = Convert.ToDouble(w.Text);
            double weighttwo = Convert.ToDouble(molw.Text);

            double calculation = weightone * weighttwo * 3.1495; 

            string pricecalculation = "$" + calculation.ToString ("#,###.00");;

            totalprice.Text = pricecalculation;
            totalprice.Visibility = System.Windows.Visibility.Visible;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            totalprice.Visibility = System.Windows.Visibility.Hidden;
            w.Text = "";
            molw.Text = "";
        }

       
    }

}
