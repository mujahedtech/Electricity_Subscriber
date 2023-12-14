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

namespace Mujahed_Package
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            logtext.Focus();
        }

        private void BtnOutMinimize_MouseMove(object sender, MouseEventArgs e)
        {
            BtnOutMinimize.Background = Brushes.White;
            BtnOutMinimize.Foreground = Brushes.Black;

        }

        private void BtnOutClose_MouseMove(object sender, MouseEventArgs e)
        {

            BtnOutClose.Background = Brushes.White;
            BtnOutClose.Foreground = Brushes.Black;
        }

        private void BtnOutClose_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            BtnOutClose.Background = (Brush)bc.ConvertFrom("#FF22859D");
            BtnOutClose.Foreground = Brushes.White;
        }

        private void BtnOutMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            BtnOutMinimize.Background = (Brush)bc.ConvertFrom("#FF22859D");
            BtnOutMinimize.Foreground = Brushes.White;
        }

        private void BtnOutClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnOutMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;

           
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserControl usc = null;
            usc = new Layouts.Home_();
            GridMain.Children.Add(usc);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

           
                UserControl usc = null;
                GridMain.Children.Clear();

                switch (ListViewMenu.SelectedIndex)

                {

                    case 0:
                        usc = new Layouts.Home_();
                        GridMain.Children.Add(usc);
                        break;

                    case 1:
                        try
                        {
                          
                            usc = new Layouts.ReceiveCash(false);
                            GridMain.Children.Add(usc);
                        }
                        catch (Exception m)
                        {

                            MessageBox.Show(m.Message);
                        }


                        break;
                    case 2:
                        usc = new Layouts.ShowSalesManCash();
                        GridMain.Children.Add(usc);
                        break;
                    case 3:
                        usc = new Layouts.SalesManReport();
                        GridMain.Children.Add(usc);
                        break;
                    case 4:
                        usc = new Layouts.Users_Management();
                        GridMain.Children.Add(usc);
                        break;
                    case 5:
                        usc = new Layouts.CashReportManual();
                        GridMain.Children.Add(usc);
                        break;
                case 6:
                    new ReportsWindow().ShowDialog();
                    break;
                default:
                        break;
                }
           
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
          

          
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
        }

        private void BtnUpdaload_Click(object sender, RoutedEventArgs e)
        {
            CL.DataBase.SentEmail();
        }

        private void logtext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                if (logtext.Text=="MujahedTech")
                {
                    LogGrid.Visibility = Visibility.Hidden;
                    logtext.Clear();
                }
                else
                {
                    logtext.Clear();
                }
            }
           
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            LogGrid.Visibility = Visibility.Visible;
        }

        private void LogGrid_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
    }
}
