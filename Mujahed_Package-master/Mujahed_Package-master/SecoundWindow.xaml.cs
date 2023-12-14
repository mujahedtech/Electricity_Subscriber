using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    public partial class SecoundWindow : Window
    {
        public SecoundWindow()
        {
            InitializeComponent();

            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            GridMain.Children.Add(new Layouts.Home_());

        }

        private void btnTopButtons(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            switch (index)
            {
                case 0:
                    if (WindowState == WindowState.Maximized)
                    {
                        WindowState = WindowState.Normal;
                        PackIconWindowsState.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
                    }
                    else if (WindowState != WindowState.Maximized)
                    {
                        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                        WindowState = WindowState.Maximized;
                        PackIconWindowsState.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowRestore;
                    }

                    break;
                case 1:
                    Application.Current.Shutdown();
                    break;
                case 2:
                    WindowState = WindowState.Minimized;
                    break;
                case 3:


                    break;
                case 4:

                    break;
            }
        }

        private void btnMainButtons(object sender, RoutedEventArgs e)
        {

            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(5 + (135 * index), 0, 0, 0);

            GridMain.Children.Clear();

            switch (index)
            {
                case 0:
                   
                    GridMain.Children.Add(new Layouts.Home_());

                    break;
                case 1:
                    GridMain.Children.Add(new Layouts.ReceiveCash());

                    break;
                case 2:
                    GridMain.Children.Add(new Layouts.ShowSalesManCash());
                    break;
                case 3:
                    GridMain.Children.Add(new Layouts.SalesManReport());
                    break;
                case 4:
                    GridMain.Children.Add(new Layouts.Users_Management());
                    break;
                case 5:
                    GridMain.Children.Add(new Layouts.ManualPrint());
                    break;
                case 6:
                    GridMain.Children.Add(new Layouts.MatchCashBill_Money());
                    break;
            }

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void logtext_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }

}