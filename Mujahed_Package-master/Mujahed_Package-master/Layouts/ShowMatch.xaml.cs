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

namespace Mujahed_Package.Layouts
{
    /// <summary>
    /// Interaction logic for ShowMatch.xaml
    /// </summary>
    /// 

    class MatchView
    {
        public string IDNumber { get; set; }
        public string DateSales { get; set; }
        public string SalesManName { get; set; }
        public string NetCashs { get; set; }
        public string NetBills { get; set; }
        public string NetAll { get; set; }
    
        public string RowColor { get; set; }
        public string TextColor { get; set; }
    }

    public partial class ShowMatch : UserControl
    {
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            GridMessage.Visibility = Visibility.Hidden;
        }
        void Message(string message)
        {
            GridMessage.Visibility = Visibility.Visible;
            MsgError.Text = message;
        }
        public ShowMatch()
        {
            InitializeComponent();

            FillClient();
        }

        //متغير من اجل ما هو وضع العرض بيانات مع تاريخ ام بدون مع مندوب ام بدون
        public bool ViewMode = false;

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (CobNameClient.SelectedIndex >= 0)
            {
                if (CobNameClient.SelectedIndex >= 0)
                {
                    int index = CobNameClient.SelectedIndex;
                    index--;
                    CobNameClient.SelectedIndex = index;
                }

            }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (CobNameClient.SelectedIndex >= 0 || CobNameClient.SelectedIndex == -1)
            {
                int index = CobNameClient.SelectedIndex;
                index++;
                CobNameClient.SelectedIndex = index;
            }
        }

        private void btnplusday(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(txtdate.Text);
            dt = dt.AddDays(1);
            txtdate.SelectedDate = dt;

         
        }

        private void btnlessday_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(txtdate.Text);
            dt = dt.AddDays(-1);
            txtdate.SelectedDate = dt;
           
        }
        private void salesmanID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                CobNameClient.Text = new CL.SalesCash().GetSalesmanNameBySYSID(salesmanID.Text);
                salesmanID.Clear();
                checkSalesMan.IsChecked = true;

            }
        }
        private void salesmanID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        public bool IsTextAllowed(string str)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[0-9/.]");

            return regex.IsMatch(str);
        }

        System.Data.DataTable DTMatchView;
        void UpdateCheckList()
        {    //                      0        1           2         3        4                                 5
            //string Columns = " IDNumber,DateSales,SalesManName,NetCashs,NetBills, (NetCashs-NetBills) as NetAll";
            List<MatchView> MatchView = new List<MatchView>();
            if (DTMatchView.Rows.Count > 0 && DTMatchView.Columns.Count > 0)
            {
                for (int i = 0; i < DTMatchView.Rows.Count; i++)
                {
                    MatchView matchview = new MatchView();

                    matchview.IDNumber = DTMatchView.Rows[i][0].ToString();
                    matchview.DateSales = DTMatchView.Rows[i][1].ToString();
                    matchview.SalesManName = DTMatchView.Rows[i][2].ToString();
                    matchview.NetCashs = DTMatchView.Rows[i][3].ToString();
                    matchview.NetBills = DTMatchView.Rows[i][4].ToString();
                    matchview.NetAll = DTMatchView.Rows[i][5].ToString();
                   

                    if (decimal.Parse(DTMatchView.Rows[i][5].ToString()) > 0)
                    {
                        matchview.RowColor = "#FF90CAF9";
                        matchview.TextColor = "Black";
                    }
                    else if (decimal.Parse(DTMatchView.Rows[i][5].ToString()) == 0)
                    {
                        matchview.RowColor = "White";
                        matchview.TextColor = "Black";
                    }
                    else if (decimal.Parse(DTMatchView.Rows[i][5].ToString()) < 0)
                    {
                        matchview.RowColor = "#FF2C6CAC";
                        matchview.TextColor = "White";
                    }
                    //check.RowColor = "#FF22849C";

                    MatchView.Add(matchview);
                }
                lbEurInsuredType.ItemsSource = null;
                lbEurInsuredType.ItemsSource = MatchView;
            }
            if (DTMatchView.Rows.Count == 0)
            {
                lbEurInsuredType.ItemsSource = null;
            }


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ViewMode = false;
            SearchStart(ViewMode);
        }

        void FillClient()
        {

            System.Data.DataTable DT = new CL.SalesCash().GetSalesmanName();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                CobNameClient.Items.Add(DT.Rows[i][0].ToString());
            }


        }

        void SearchStart(bool LessCash)
        {
            try
            {
                DTMatchView = new CL.BillsCashs().GetCashMaster("-1");
                DateTime dateTime = Convert.ToDateTime(txtdate.Text);
                System.Data.DataView DV = new System.Data.DataView(DTMatchView);

                //هنا عرض جميع البيانات 
                if (LessCash == false)
                {

                    if (checkSalesMan.IsChecked == true)
                    {
                        if (CobNameClient.SelectedIndex == -1)
                        {
                            CobNameClient.SelectedIndex = 0;

                        }
                        if (CobNameClient.SelectedIndex != -1)
                        {
                            if (checkLessCash.IsChecked == true)
                            {
                                DV.RowFilter = $"SalesManName like '%{CobNameClient.Text}%' ";

                            }
                            else if (checkLessCash.IsChecked == false)
                            {
                                DV.RowFilter = $"(DateSales <= #{dateTime.ToString(CL.PassParameters.DateFormat)}# and DateSales >= #{dateTime.ToString(CL.PassParameters.DateFormat)}#) and SalesManName like '%{CobNameClient.Text}%' ";
                            }

                        }

                    }
                    else if (checkSalesMan.IsChecked == false)
                    {
                        if (checkLessCash.IsChecked == true)
                        {
                            DV.RowFilter = $" ";

                        }
                        else if (checkLessCash.IsChecked == false)
                        {
                            DV.RowFilter = $"DateSales <= #{dateTime.ToString(CL.PassParameters.DateFormat)}# and DateSales >= #{dateTime.ToString(CL.PassParameters.DateFormat)}# ";
                        }

                    }

                }
                //عرض البيانات للمناديب التي توجد نقص في غلاتهم
                else if (LessCash == true)
                {

                    if (checkSalesMan.IsChecked == true)
                    {
                        if (CobNameClient.SelectedIndex == -1)
                        {
                            CobNameClient.SelectedIndex = 0;

                        }
                        if (CobNameClient.SelectedIndex != -1)
                        {
                            DV.RowFilter = $"SalesManName like '%{CobNameClient.Text}%' and NetAll < 0";
                        }

                    }
                    else if (checkSalesMan.IsChecked == false)
                    {
                        if (checkLessCash.IsChecked == true)
                        {
                            DV.RowFilter = $"NetAll < 0";

                        }
                        else if (checkLessCash.IsChecked == false)
                        {
                            DV.RowFilter = $"(DateSales <= #{dateTime.ToString(CL.PassParameters.DateFormat)}# and DateSales >= #{dateTime.ToString(CL.PassParameters.DateFormat)}#)" +
                           $" and NetAll < 0";
                        }

                    }

                }
                DTMatchView = DV.ToTable();
                UpdateCheckList();

            }
            catch (Exception m)
            {
               Message(m.Message);
            }
        }

        private void btnCashLess_Click(object sender, RoutedEventArgs e)
        {
            ViewMode = true;
            SearchStart(ViewMode);
        }

        private void btnDeleteCheck_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you Sure", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var button = sender as System.Windows.Controls.Button;
                var Check = button.CommandParameter as MatchView;

                new CL.BillsCashs().DeleteDetails(Check.IDNumber);

                SearchStart(ViewMode);
            }
           
            
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var Check = button.CommandParameter as MatchView;

            CL.PassParameters.IDSerialMatch = Check.IDNumber;
            //CL.PassParameters.UpdateMode = true;
            GridViewData.Children.Clear();
            GridViewData.Children.Add(new Layouts.MatchCashBill_Money());
            GridViewData.Visibility = Visibility.Visible;
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            GridViewData.Visibility = Visibility.Collapsed;
        }
    }
}
