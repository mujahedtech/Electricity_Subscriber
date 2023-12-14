using Microsoft.Reporting.WinForms;
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

namespace AccountingSystem.Reports
{
    /// <summary>
    /// Interaction logic for MainReportsWindow.xaml
    /// </summary>
    public partial class MainReportsWindow : Window
    {
        public MainReportsWindow()
        {
            InitializeComponent();

            Loaded += MainReportsWindow_Loaded;
        }


       

        private void MainReportsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
        }
    }
}
