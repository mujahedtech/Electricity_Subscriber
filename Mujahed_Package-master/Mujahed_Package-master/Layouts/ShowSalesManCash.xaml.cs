using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Office.Interop.Excel;





namespace Mujahed_Package.Layouts
{
    /// <summary>
    /// Interaction logic for ShowSalesManCash.xaml
    /// </summary>
    /// 

    class SalesMan
    {
        
        public string IDNumber { get; set; }
        public string DateSales { get; set; }
        public string SalesManName { get; set; }
        public string TotalZakah { get; set; }
        public string TotalRequiredCash { get; set; }
        public string TotalCashOnHand { get; set; }
        public string TotalEndCash { get; set; }

        public string RowColor { get; set; }
        public string TextColor { get; set; }
    }
    public partial class ShowSalesManCash : UserControl
    {
        System.Data.DataTable DTCash = new System.Data.DataTable();
        public ShowSalesManCash()
        {
            InitializeComponent();

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        //handle All Error In Message
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //try
            //{
            //    Exception ex = e.ExceptionObject as Exception;
            //    MessageBox.Show(ex.Message, "Uncaught Thread Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            //    MessageBox.Show("Application Will ShutDown !!!", "ShutDown", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //catch (Exception)
            //{


            //}

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

   

            //DTChecks = new CL.SalesCash().GetCash();
            FillClient();
            GridlistChecks.Visibility = Visibility.Visible;

            //UpdateCheckList();

            //listtest.Height = GridlistChecks.RowDefinitions[2].ActualHeight - 10;

          

            listtest.Visibility = Visibility.Visible;
            lbEurInsuredType.Visibility = Visibility.Collapsed;

            RoutedEventArgs newEventArgs = new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent);
            btnSearch.RaiseEvent(newEventArgs);

           

           


            

        }
        System.Data.DataTable DTSalesMan;
         void FillClient()
        {

            DTSalesMan = new CL.SalesCash().GetSalesmanName();
            for (int i = 0; i < DTSalesMan.Rows.Count; i++)
            {
                CobNameClient.Items.Add(DTSalesMan.Rows[i][0].ToString());
            }

            
        }

        System.Data.DataTable DTChecks = new System.Data.DataTable();
        public bool IsTextAllowed(string str)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[0-9/.]");

            return regex.IsMatch(str);
        }


        System.Data.DataTable DTAlter = new System.Data.DataTable();
        void CreateColumns()
        {
           
            DTAlter.Columns.Add("IDNumber", typeof(System.Int32));
            DTAlter.Columns.Add("DateSales", typeof(System.DateTime));
            DTAlter.Columns.Add("SalesManName", typeof(System.String));
            DTAlter.Columns.Add("TotalZakah", typeof(System.Decimal));
            DTAlter.Columns.Add("TotalRequiredCash", typeof(System.Decimal));
            DTAlter.Columns.Add("TotalCashOnHand", typeof(System.Decimal));
            DTAlter.Columns.Add("TotalEndCash", typeof(System.Decimal));
            DTAlter.Columns.Add("Cash50", typeof(System.Decimal));
            DTAlter.Columns.Add("Cash20", typeof(System.Decimal));
            DTAlter.Columns.Add("Cash10", typeof(System.Decimal));
            DTAlter.Columns.Add("Cash5", typeof(System.Decimal));
            DTAlter.Columns.Add("Cash1", typeof(System.Decimal));
            DTAlter.Columns.Add("CashNon", typeof(System.Decimal));
            DTAlter.Columns.Add("SalesNote", typeof(System.String));
            DTAlter.Columns.Add("ImageCash", typeof(System.String));
            DTAlter.Columns.Add("SYSID", typeof(System.String));


        }


        void UpdateCheckList()
        {



       
          

            List<SalesMan> checks = new List<SalesMan>();
            if (DTChecks.Rows.Count > 0 && DTChecks.Columns.Count > 0)
            {
                for (int i = 0; i < DTChecks.Rows.Count; i++)
                {
                    SalesMan check = new SalesMan();
                  
                    check.IDNumber = DTChecks.Rows[i][0].ToString();
                    check.DateSales = DTChecks.Rows[i][1].ToString();
                    check.SalesManName = DTChecks.Rows[i][2].ToString();
                    check.TotalZakah = DTChecks.Rows[i][3].ToString();
                    check.TotalRequiredCash = DTChecks.Rows[i][4].ToString();
                    check.TotalCashOnHand = DTChecks.Rows[i][5].ToString();
                    check.TotalEndCash = DTChecks.Rows[i][6].ToString();

                    if (decimal.Parse(DTChecks.Rows[i][6].ToString())>0)
                    {
                        check.RowColor = "#778899";
                        check.TextColor = "White";
                    }
                   else if (decimal.Parse(DTChecks.Rows[i][6].ToString()) == 0)
                    {
                        check.RowColor = "#eee";
                        check.TextColor = "Black";
                    }
                   else if (decimal.Parse(DTChecks.Rows[i][6].ToString()) < 0)
                    {
                        check.RowColor = "#FF2C6CAC";
                        check.TextColor = "White";
                    }
                    //check.RowColor = "#FF22849C";

                    checks.Add(check);
                }
               
                listtest.ItemsSource = null;
                listtest.ItemsSource = checks;

                lbEurInsuredType.ItemsSource = null;
                lbEurInsuredType.ItemsSource = checks;
            }
            if (DTChecks.Rows.Count == 0)
            {
                listtest.ItemsSource = null;
                lbEurInsuredType.ItemsSource = null;
            }


        }

        private void BtnDeleteCheck_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you Sure", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var button = sender as System.Windows.Controls.Button;
                var Check = button.CommandParameter as SalesMan;
                new CL.SalesCash().DeleteSaleCash(Check.IDNumber);
                SearchStart(ViewMode);
            }
           
        }

        //متغير من اجل ما هو وضع العرض بيانات مع تاريخ ام بدون مع مندوب ام بدون
        public bool ViewMode = false;


        //متغير يتم حفظ فيه رقم الحركة من اجل عرضها للمستخدم في حال اراد تعديل
        string IDSerial;
        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var Check = button.CommandParameter as SalesMan;
            CL.PassParameters.IDSerial = Check.IDNumber;
            IDSerial =Check.IDNumber;

            

            txtnote.Text = new CL.SalesCash().GetCashNote(IDSerial).Rows[0][0].ToString();
            txtnote.Focus();

            CardPrepareShow.Visibility = Visibility.Visible;

            listZakah.ItemsSource =new CL.SalesCash().GetZakahPerID(IDSerial).DefaultView;

        }
        private void btnShowData_Click(object sender, RoutedEventArgs e)
        {
           

            
           
            Layouts.ReceiveCash receiveCash = new ReceiveCash(IDSerial);
            receiveCash.DTCustomData = DTChecks;
            //receiveCash.SelectIndexDT = lbEurInsuredType.SelectedIndex;
            
           

            gridviewdata.Children.Clear();
            gridviewdata.Children.Add(receiveCash);

            CardPrepareShow.Visibility = Visibility.Collapsed;

            gridview.Visibility = Visibility.Visible;
        }


        private void Btnclose_Click(object sender, RoutedEventArgs e)
        {
            gridview.Visibility = Visibility.Collapsed;
        }


        string SYSIDstr = "";
        void SearchStart(bool LessCash)
        {
            
               

               

                string salesMan = $" (SYSID ='{salesmanID.Text}')";

                DTChecks = new CL.SalesCash().GetCash();
                DateTime DateFrom = Convert.ToDateTime(txtdate.Text);

                DateTime DateTo = Convert.ToDateTime(txtdateTo.Text);
                System.Data.DataView DV = new System.Data.DataView(DTChecks);

                //هنا عرض جميع البيانات 
                if (LessCash==false)
                {
                   
                    if (checkSalesMan.IsChecked == true)
                    {
                        if (CobNameClient.SelectedIndex == -1)
                        {
                            CobNameClient.SelectedIndex = 0;

                        }
                        if (CobNameClient.SelectedIndex != -1)
                        {
                            if (CheckWithDate.IsChecked == true)
                            {
                                DV.RowFilter = $"{salesMan} ";

                            }
                            else if (CheckWithDate.IsChecked == false)
                            {
                                DV.RowFilter = $"(DateSales >= #{DateFrom.ToString(CL.PassParameters.DateFormat)}# and DateSales <= #{DateTo.ToString(CL.PassParameters.DateFormat)}#) and {salesMan} ";
                            }
                            
                        }

                    }
                    else if (checkSalesMan.IsChecked == false)
                    {
                        if (CheckWithDate.IsChecked == true)
                        {
                            DV.RowFilter = $" ";

                        }
                        else if (CheckWithDate.IsChecked == false)
                        {
                            DV.RowFilter = $"DateSales >= #{DateFrom.ToString(CL.PassParameters.DateFormat)}# and DateSales <= #{DateTo.ToString(CL.PassParameters.DateFormat)}# ";
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
                            if (CheckWithDate.IsChecked == true)
                            {
                                DV.RowFilter = $"{salesMan} and TotalEndCash < 0";

                            }
                            else if (CheckWithDate.IsChecked == false)
                            {
                                DV.RowFilter = $"(DateSales >= #{DateFrom.ToString(CL.PassParameters.DateFormat)}# and DateSales <= #{DateTo.ToString(CL.PassParameters.DateFormat)}#)" +
                               $" and TotalEndCash < 0 and {salesMan}";
                            }
                         
                        }

                    }
                    else if (checkSalesMan.IsChecked == false)
                    {
                        if (CheckWithDate.IsChecked==true)
                        { 
                            DV.RowFilter = $"TotalEndCash < 0";
                            
                        }
                        else if (CheckWithDate.IsChecked == false)
                        {
                            DV.RowFilter = $"(DateSales >= #{DateFrom.ToString(CL.PassParameters.DateFormat)}# and DateSales <= #{DateTo.ToString(CL.PassParameters.DateFormat)}#)" +
                           $" and TotalEndCash < 0";
                        }
                       
                    }

                }
                DTChecks = DV.ToTable();
                UpdateCheckList();

           
        }

       

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (CobNameClient.SelectedIndex>=0)
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

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            
        }


        void ExportExcel()
        {
            if (DTChecks.Rows.Count > 0)
            {
               
                int Count_Rows = 0;
                

                Microsoft.Office.Interop.Excel.Application XLS = new Microsoft.Office.Interop.Excel.Application();
                Workbook WB = XLS.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet WS = (Worksheet)XLS.ActiveSheet;

                //عنوان الجدول
                Microsoft.Office.Interop.Excel.Range Header;
                Header = WS.Range["A1", "O1"];
                Header.Font.Bold = true;
                Header.Rows.RowHeight = 30;
                Header.Interior.Color = XlRgbColor.rgbLightGray;




                //تنسيق الجدول
                Range RA;
                Count_Rows = DTChecks.Rows.Count;
                Count_Rows += 1;
                RA = WS.Range["A1", "O" + Count_Rows];
                WS.Columns.Font.Size = 12;
                RA.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                RA.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                RA.Columns.Borders.Color = System.Drawing.Color.Black;

                for (int i = 0; i < DTChecks.Columns.Count; i++)
                {
                    XLS.Cells[1, i + 1] = DTChecks.Columns[i].Caption;
                }
                for (int i = 0; i < DTChecks.Rows.Count; i++)
                {
                    for (int j = 0; j < DTChecks.Columns.Count; j++)
                    {

                        XLS.Cells[i + 2, j + 1] = DTChecks.Rows[i][j].ToString();
                    }
                   
                }

                XLS.Visible = true;
                XLS.WindowState = XlWindowState.xlMaximized;
                XLS.Columns.AutoFit();
            }
            else if (DTChecks.Rows.Count == 0)
            {
                System.Windows.MessageBox.Show("لا يوجد اي بيانات ليتم تحويلها الى اكسل");
            }

        }

        private void btnCashLess_Click(object sender, RoutedEventArgs e)
        {
            ViewMode = true;

            SearchStart(ViewMode);


          

        }

       

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            CardPrepareShow.Visibility = Visibility.Collapsed;
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

        private void txtdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void salesmanID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void salesmanID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                
                    CobNameClient.Text = new CL.SalesCash().GetSalesmanNameBySYSID(salesmanID.Text);
                   
                checkSalesMan.IsChecked = true;
                
            }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            CL.PassParameters.DTReport = DTChecks;
            if (CL.PassParameters.DTReport.Rows.Count>0 && CL.PassParameters.DTReport.Columns.Count > 0)
            {
                new ReportsWindow().Show();
            }
           
        }

        private void txtdate_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnRestDate2020_Click(object sender, RoutedEventArgs e)
        {



            txtdate.SelectedDate = new DateTime(DateTime.Now.Year, 1, 1);
        }

        private void lbEurInsuredType_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void lbEurInsuredType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lbEurInsuredType.SelectedIndex > -1)
            //{
            //    IDSerial = DTChecks.Rows[lbEurInsuredType.SelectedIndex][0].ToString();
            //    CL.PassParameters.IDSerial = IDSerial;


            //    //GridPrepareShow.Visibility = Visibility.Visible;

            //    //txtnote.Text = new CL.SalesCash().GetCashNote(IDSerial).Rows[0][0].ToString();
            //    //txtnote.Focus();


            //    RoutedEventArgs newEventArgs = new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent);
            //    btnShowData.RaiseEvent(newEventArgs);
            //    //btnShowData_Click
            //}

        }

        private void lbEurInsuredType_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
          
           
        }

        private void CobNameClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            salesmanID.Text = DTSalesMan.Rows[CobNameClient.SelectedIndex][1].ToString();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ViewMode = false;

            SearchStart(ViewMode);

        }

        private void btnChangeView_Click(object sender, RoutedEventArgs e)
        {
            if (listtest.Visibility == Visibility.Visible)
            {
                listtest.Visibility = Visibility.Collapsed;
                lbEurInsuredType.Visibility = Visibility.Visible;
            }
            else if (lbEurInsuredType.Visibility == Visibility.Visible)
            {
                listtest.Visibility = Visibility.Visible;
                lbEurInsuredType.Visibility = Visibility.Collapsed;
            }
        }
    }
}
