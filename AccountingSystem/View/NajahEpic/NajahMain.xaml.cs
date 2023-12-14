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

namespace AccountingSystem.View.NajahEpic
{
    /// <summary>
    /// Interaction logic for NajahMain.xaml
    /// </summary>
    public partial class NajahMain : UserControl
    {
        public NajahMain()
        {
            InitializeComponent();
        }

        private void btnMainButtons(object sender, RoutedEventArgs e)
        {

            int index = int.Parse(((Button)e.Source).Uid);


            //GridCursor.Margin = new Thickness(5 + (140 * index), 0, 0, 0);



            switch (index)
            {
                case 0:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new NajahEpic.DailyTransaction());



                    break;
                case 1:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new NajahEpic.DebitManage());

                    break;
                case 2:
                    GridMain.Children.Clear();
                    //GridMain.Children.Add(new Reports.MainReports());
                    break;
                case 3:

                    break;
                case 4:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new PurchaseManage.MainPurchase());
                    break;
                case 5:
                    GridMain.Children.Clear();
                    break;
                case 6:
                    GridMain.Children.Clear();

                    break;
            }

        }

    }

}
