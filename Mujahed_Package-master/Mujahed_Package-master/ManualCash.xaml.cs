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
using System.Windows.Shapes;

namespace Mujahed_Package
{
    /// <summary>
    /// Interaction logic for ManualCash.xaml
    /// </summary>
    public partial class ManualCash : Window
    {
        public ManualCash()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                
                if (printDialog.ShowDialog() == true)
                {
                 
                    printDialog.PrintVisual(MainGrid, "invoice");
                }



            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
