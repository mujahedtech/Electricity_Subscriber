using Electricity_Subscriber.CL;
using mshtml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Printing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Electricity_Subscriber.Layouts
{
    public class Years
    {

        public int SYSID { get; set; }

        public string YearName { get; set; }

    }
    public class Months
    {

        public int SYSID { get; set; }

        public string MonthName { get; set; }

    }

    public class ListSelectedDate
    {

        public int SYSID { get; set; }

        public string MonthName { get; set; }

        public string YearName { get; set; }

        public decimal BillAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentNote { get; set; }

        public string PaymentMethod { get; set; }


    }


    public class SubscribeInfo
    {
        public int Index { get; set; }
        public DateTime DateBill { get; set; }
        public int Consume_KW { get; set; }
        public double BillAmount { get; set; }
        public double PaidAmount { get; set; }
        public double RequiredAmount { get; set; }
        public string PaidDate { get; set; }


    }

    public class MVVM
    {
        public IEnumerable<Months> MonthList { get => GetMonths(); }
        public IEnumerable<Months> GetMonths()
        {

            var trendList = new System.Collections.ObjectModel.ObservableCollection<Months>();

            trendList = new System.Collections.ObjectModel.ObservableCollection<Months>();
            trendList.Add(new Months { SYSID = 1, MonthName = "1" });
            trendList.Add(new Months { SYSID = 2, MonthName = "2" });
            trendList.Add(new Months { SYSID = 3, MonthName = "3" });
            trendList.Add(new Months { SYSID = 4, MonthName = "4" });
            trendList.Add(new Months { SYSID = 5, MonthName = "5" });
            trendList.Add(new Months { SYSID = 6, MonthName = "6" });
            trendList.Add(new Months { SYSID = 7, MonthName = "7" });
            trendList.Add(new Months { SYSID = 8, MonthName = "8" });
            trendList.Add(new Months { SYSID = 9, MonthName = "9" });
            trendList.Add(new Months { SYSID = 10, MonthName = "10" });
            trendList.Add(new Months { SYSID = 11, MonthName = "11" });
            trendList.Add(new Months { SYSID = 12, MonthName = "12" });



            return trendList;
        }



        public IEnumerable<Years> YearList { get => GetYears(); }
        public IEnumerable<Years> GetYears()
        {

            var trendList = new System.Collections.ObjectModel.ObservableCollection<Years>();

            trendList = new System.Collections.ObjectModel.ObservableCollection<Years>();
            trendList.Add(new Years { SYSID = 1, YearName = DateTime.Now.AddYears(-4).Year.ToString() });
            trendList.Add(new Years { SYSID = 1, YearName = DateTime.Now.AddYears(-3).Year.ToString() });
            trendList.Add(new Years { SYSID = 1, YearName = DateTime.Now.AddYears(-2).Year.ToString() });
            trendList.Add(new Years { SYSID = 1, YearName = DateTime.Now.AddYears(-1).Year.ToString() });
            trendList.Add(new Years { SYSID = 1, YearName = DateTime.Now.AddYears(0).Year.ToString() });


            return trendList;
        }

    }
    /// <summary>
    /// Interaction logic for CustomReport.xaml
    /// </summary>
    public partial class CustomReport : UserControl
    {
        public CustomReport(string ID = "", string NameSub = "")
        {
            InitializeComponent();

            DataContext = new MVVM();

            txtID.Text = ID;
            txtNameSub.Text = NameSub;





        }


        string URL = "";
        string GenerateURL(string SubscriberNumber, string Month, string Year)
        {
            string ENDURL = "";
            string CityID = "", CustomerID = "";
            string FirstURL = "https://www.ideco.com.jo/portal/WebForms/SubInvoiceDetails.aspx?";


            string Number = SubscriberNumber;

            CityID = Number.Substring(0, 3);





            CustomerID = Number.Substring(3, 7);








            int numbercon = int.Parse(CustomerID);

            CustomerID = numbercon.ToString();






            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }

            ENDURL = $"IssuYM={Year + Month}&CityId={CityID}&CusmId={CustomerID}";


            System.IO.File.AppendAllText("mujahed.txt", FirstURL + ENDURL + Environment.NewLine);

            return URL = FirstURL + ENDURL;
        }





        System.Collections.ObjectModel.ObservableCollection<ListSelectedDate> trendList = new System.Collections.ObjectModel.ObservableCollection<ListSelectedDate>();
        List<DateTime> GetMonthsBetween(DateTime from, DateTime to)
        {
            int Counter = 1;
            trendList = new System.Collections.ObjectModel.ObservableCollection<ListSelectedDate>();
            list1.Items.Clear();
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                monthDiff -= 1;
            }

            List<DateTime> results = new List<DateTime>();
            for (int i = monthDiff; i >= 0; i--)
            {
                //results.Add(to.AddMonths(-i));

                list1.Items.Add(to.AddMonths(-i).ToString("[MM] yyyy"));




                CL.ElectricData ElectricData = new CL.ElectricData(GenerateURL(txtID.Text, to.AddMonths(-i).ToString("MM"), to.AddMonths(-i).ToString("yyyy")));


                trendList.Add(new ListSelectedDate
                {
                    SYSID = Counter,
                    MonthName = to.AddMonths(-i).ToString("MM"),
                    YearName = to.AddMonths(-i).ToString("yyyy"),
                    BillAmount = decimal.Parse(ElectricData.BillAmount),
                    PaidAmount = decimal.Parse(ElectricData.PaidAmount),
                    TotalAmount = decimal.Parse(ElectricData.TotalAmount),
                    PaymentNote = ElectricData.PaymentNote,
                    PaymentMethod = ElectricData.PaymentMethod
                });
                Counter++;



            }


            trendList.Add(new ListSelectedDate
            {
                SYSID = 0,
                MonthName = "",
                YearName = "مجموع",
                BillAmount = trendList.Sum(i => i.BillAmount),
                PaidAmount = trendList.Sum(i => i.PaidAmount),
                TotalAmount = trendList.Sum(i => i.TotalAmount),
                PaymentNote = "",
                PaymentMethod = ""
            }); ;

            return results;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.IsEnabled = false;
            //MainGrid.Background = Brushes.White;

            //this.IsEnabled = true;
            //MainGrid.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF5E86A6");




            //List<string> Paths = new List<string>();
            //Paths.Add("print_previw.xps");


            //BatchAddToPrintQueue(Paths, false);

            //PrintDialog pd = new PrintDialog();



            //pd.PrintVisual(Datagrid1, "My Print");




            new Layouts.PrintSingle(tempLocaL, txtNameSub.Text, txtID.Text) { WindowState = WindowState.Maximized }.ShowDialog();


        }







        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            txtMonthFrom.Text = "1";



            txtYearFrom.Text = DateTime.Now.AddYears(-2).Year.ToString();

            string today = DateTime.Now.AddMonths(-1).ToString("MM");




            txtMonthTo.Text = (today.StartsWith("0") ? today.Replace("0", "") : today).ToString();



            txtYearTo.Text = DateTime.Now.Year.ToString();




        }







        private void Datagrid1_AutoGeneratedColumns(object sender, EventArgs e)
        {
            //Datagrid1.Columns[0].Header = "تسلسل";
            //Datagrid1.Columns[1].Header = "الشهر";
            //Datagrid1.Columns[2].Header = "السنة";
            //Datagrid1.Columns[3].Header = "قيمة الاشتراك";
            //Datagrid1.Columns[4].Header = "القيمة المدفوعة";
            //Datagrid1.Columns[5].Header = "صافي الفاتورة";
            //Datagrid1.Columns[6].Header = "تاريخ التسديد";
            //Datagrid1.Columns[7].Header = "آلية التسديد";
        }

        string Path = "https://www.ideco.com.jo/portal/WebForms/SubscriberReceivableLinks.aspx";


        System.Windows.Forms.WebBrowser browser1;
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            AcceptRun = false;

            Datagrid1.ItemsSource = null;

            browser1 = new System.Windows.Forms.WebBrowser { ScriptErrorsSuppressed = true };

           

            browser1.Navigate(new Uri(Path));

            browser1.DocumentCompleted += Browser1_DocumentCompleted;
            gridview.Visibility = Visibility.Visible;

        }

        bool AcceptRun = false;

        DataTable DtData;


        ObservableCollection<SubscribeInfo> subscribes = new ObservableCollection<SubscribeInfo>();
        private void Browser1_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            

            if (AcceptRun)
            {
                System.IO.File.WriteAllText("html.txt", "");
                System.IO.File.AppendAllText("html.txt", browser1.DocumentText + Environment.NewLine);

                DtData = new Show_Subscriber().GetPaidTest(browser1.DocumentText);
                DtData.Merge(new Show_Subscriber().GetUnPaidTest(browser1.DocumentText));


                //DtData.Columns.Remove("كمية الطاقة المصدرة");
                //DtData.Columns.Remove("كمية الطاقة المستجرة");
                //DtData.Columns.Remove("القيمة المطلوبة");
                //DtData.Columns.Remove("اختيار");

                DtData.Columns["كمية الاستهلاك المحتسبة"].ColumnName = "Consume_KW";



                var temp = (from DataRow row in DtData.Rows

                            select new SubscribeInfo
                            {
                                Index = 1,
                                DateBill = DateTime.Parse(row["DateBill"].ToString()),
                                Consume_KW = int.Parse(row["Consume_KW"].ToString()),
                                BillAmount = double.Parse(row["BillAmount"].ToString()),
                                PaidAmount = Validations.IsDecimal(row["PaidAmount"].ToString())==true? double.Parse(row["PaidAmount"].ToString()):0,
                                RequiredAmount = double.Parse(row["RequiredAmount"].ToString()),
                                PaidDate = row["PaidDate"].ToString()

                            }).ToList();



                subscribes = new ObservableCollection<SubscribeInfo>(temp.OrderByDescending(i => i.DateBill));

                tempLocaL = subscribes.ToList();


                //DataView dv = DtData.DefaultView;
                //    dv.Sort = "DateBill desc";

                Datagrid1.ItemsSource = subscribes;

                btnDateFilter.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));

                gridview.Visibility = Visibility.Collapsed;

                return;

            }

            else
            {
              

                HTMLDocument dom = (HTMLDocument)browser1.Document.DomDocument;



                object ie = dom.getElementById("ContentPlaceHolder1_txtCustomerNo");
                if (ie != null)
                {
                    if (ie is IHTMLInputElement)
                    {
                        ((IHTMLInputElement)ie).value = txtID.Text;
                    }
                }

               

                object[] args = { "ctl00$ContentPlaceHolder1$btnGetInvoices2", "", true, "Group2", "", false, false };

                browser1.Document.InvokeScript("__doPostBack", args);

                AcceptRun = true;

                return;
            }






        }

        List<SubscribeInfo> tempLocaL;
        private void btnDateFilter_Click(object sender, RoutedEventArgs e)
        {
            int SelectedMonthFrom =int.Parse(txtMonthFrom.Text);
            int SelectedMonthTo =int.Parse( txtMonthTo.Text);

            int SelectedYearFrom = int.Parse(txtYearFrom.Text);
            int SelectedYearTo = int.Parse(txtYearTo.Text);

            DateTime DateFrom = new DateTime(SelectedYearFrom, SelectedMonthFrom, 1);
            DateTime DateTo = new DateTime(SelectedYearTo, SelectedMonthTo,1);

            tempLocaL = subscribes.ToList();


            tempLocaL = tempLocaL.Where(i => i.DateBill >= DateFrom&& i.DateBill <= DateTo).ToList();

          


            Datagrid1.ItemsSource = tempLocaL.OrderByDescending(i => i.DateBill); ;
        }

        private void btnDateClearFilter_Click(object sender, RoutedEventArgs e)
        {
            Datagrid1.ItemsSource = subscribes.OrderByDescending(i=>i.DateBill);


        }
    }
}
