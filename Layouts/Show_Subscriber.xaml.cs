using Electricity_Subscriber.CL;
using HtmlAgilityPack;
using mshtml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

    public class SubscriberTypeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }


    /// <summary>
    /// Interaction logic for Show_Subscriber.xaml
    /// </summary>
    public partial class Show_Subscriber : UserControl
    {


        string SubscriberSelected = "";


        public Show_Subscriber(string subscriberselected = "")
        {
            InitializeComponent();


            string ToolTipSearch = "";

            ToolTipSearch += "لعرض الحذف اكتب 'delete'" + Environment.NewLine;
            ToolTipSearch += "لعرض المفضلة اكتب '*'" + Environment.NewLine;
            ToolTipSearch += "لعرض الكل اترك فارغ " + Environment.NewLine;


            txtSearch.ToolTip = ToolTipSearch;



            BW.DoWork += BW_DoWork;

            BW.ProgressChanged += BW_ProgressChanged;

            BW.RunWorkerCompleted += BW_RunWorkerCompleted;

            BW.WorkerReportsProgress = true;

            if (subscriberselected != "")
            {
                SubscriberSelected = subscriberselected;
            }





            LoadedPrepare();
        }


        System.Data.DataTable DT = new System.Data.DataTable();



        DataTable UpdateList(string SQLCOmmand)
        {

            if (SQLCOmmand.Contains("Where") == false)
            {
                SQLCOmmand = "Where " + SQLCOmmand;
            }

            DT = new CL.DatabaseQueries().GetInfoAllWithWhere(SQLCOmmand);



            return DT;
        }


        string SQLCommand = "";





        string Path = "https://www.ideco.com.jo/portal/WebForms/SubscriberReceivableLinks.aspx";

        System.Windows.Forms.WebBrowser browser1;


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            SQLCommand = $"TypeSubscriber like '{txtType.Text}' AND NoteSubscriber <> 'delete'";

            Datagrid1.ItemsSource = UpdateList(SQLCommand).DefaultView;

        }
        private void fastSearch_Click(object sender, RoutedEventArgs e)
        {

            SQLCommand = $"TypeSubscriber like '{txtType.Text}' AND NoteSubscriber <> 'delete'";

            Datagrid1.ItemsSource = UpdateList(SQLCommand).DefaultView;

            gridview.Visibility = Visibility.Visible;

            MovetoRowDataGridByIndex(0);


            AcceptRun = false;

            browser1 = new System.Windows.Forms.WebBrowser { ScriptErrorsSuppressed = true };

            browser1.Navigate(new Uri(Path));


            browser1.DocumentCompleted += Browser1_DocumentCompleted;



            //StartFastSearch();

        }

        bool AcceptRun = false;
        private void Browser1_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {

            if (AcceptRun)
            {




                string month = txtMonth.Text;
                if (month.Length == 1)
                {
                    month = "0" + month;
                }

                DataTable dt = GetPaidTest(browser1.DocumentText);
                dt.Merge(GetUnPaidTest(browser1.DocumentText));


                //Window test = new Window();

                //test.Content = new DataGrid { ItemsSource = dt.DefaultView };

                //test.Show();




                double PaidAmount = 0;




                double PreviousAmount = 0;

                if (dt.Columns.Contains("RequiredAmount"))
                {
                    PreviousAmount = string.IsNullOrEmpty(dt.Compute("sum(RequiredAmount)", $"DateBill<='{txtYear.Text}/{month}/1'").ToString()) ? 0 : double.Parse(dt.Compute("sum(RequiredAmount)", $"DateBill<='{txtYear.Text}/{month}/1'").ToString());

                }


                if (dt.Columns.Contains("PaidAmount"))
                {
                    PaidAmount = string.IsNullOrEmpty(dt.Compute("sum(PaidAmount)", $"DateBill='{txtYear.Text}/{month}'").ToString()) ? 0 : double.Parse(dt.Compute("sum(PaidAmount)", $"DateBill='{txtYear.Text}/{month}'").ToString());

                }

                double BillAmount = string.IsNullOrEmpty(dt.Compute("sum(BillAmount)", $"DateBill='{txtYear.Text}/{month}'").ToString()) ? 0 : double.Parse(dt.Compute("sum(BillAmount)", $"DateBill='{txtYear.Text}/{month}'").ToString());
                //double PreviousAmount = string.IsNullOrEmpty(dt.Compute("sum(RequiredAmount)", $"(PaidDate='غير مسددة') and DateBill<>'{txtYear.Text}/{month}'").ToString()) ? 0 : double.Parse(dt.Compute("sum(RequiredAmount)", $"(PaidDate='غير مسددة') and DateBill<>'{txtYear.Text}/{month}'").ToString());

                DataView dv = new DataView(dt);

                dv.RowFilter = $"DateBill='{txtYear.Text}/{month}'";

                string PaidDate = "";

                string RequiredAmount = "0";

                if (dv.ToTable().Rows.Count > 0)
                {
                    PaidDate = dv.ToTable().Rows[0]["PaidDate"].ToString();
                    RequiredAmount = dv.ToTable().Rows[0]["RequiredAmount"].ToString();



                }





                DT.Rows[Datagrid1.SelectedIndex]["SubscriberPaid"] = PaidDate;

                if (PaidDate == "مسددة")
                {
                    PaidAmount = BillAmount;
                }


                DT.Rows[Datagrid1.SelectedIndex]["BillAmount"] = BillAmount.ToString("0.000");
                DT.Rows[Datagrid1.SelectedIndex]["PaidAmount"] = PaidAmount.ToString("0.000");
                DT.Rows[Datagrid1.SelectedIndex]["TotalAmount"] = PreviousAmount.ToString("0.000");
                DT.Rows[Datagrid1.SelectedIndex]["PreviousAmount"] = (PreviousAmount - BillAmount) > 0 ? (PreviousAmount - BillAmount).ToString("0.000") : "0";









                Datagrid1.ItemsSource = DT.DefaultView;


                if (Datagrid1.SelectedIndex + 1 == DT.Rows.Count)
                {

                    txtTotal.Text = DT.Compute("Sum(TotalAmount)", "").ToString();
                    txtTotalBill.Text = DT.Compute("Sum(BillAmount)", "").ToString();



                    DT.Rows.Add(null, null, "مجموع", "", DT.Compute("Sum(PreviousAmount)", ""), DT.Compute("Sum(BillAmount)", ""), DT.Compute("Sum(PaidAmount)", ""), DT.Compute("Sum(TotalAmount)", ""), "", "", "", "", "");




                    gridview.Visibility = Visibility.Collapsed;
                    return;
                }

                MovetoRowDataGridByIndex(Datagrid1.SelectedIndex + 1);

                Datagrid1.ScrollIntoView(Datagrid1.SelectedItem);



                browser1.Navigate(new Uri(Path));

                txtTotal.Text = DT.Compute("Sum(TotalAmount)", "").ToString();




                AcceptRun = false;
            }

            else
            {

                HTMLDocument dom = (HTMLDocument)browser1.Document.DomDocument;
                object ie = dom.getElementById("ContentPlaceHolder1_txtCustomerNo");
                if (ie != null)
                {
                    if (ie is IHTMLInputElement)
                    {
                        ((IHTMLInputElement)ie).value = DT.Rows[Datagrid1.SelectedIndex]["NumberSubscriber"].ToString();
                    }
                }

                object[] args = { "ctl00$ContentPlaceHolder1$btnGetInvoices2", "", true, "Group2", "", false, false };

                browser1.Document.InvokeScript("__doPostBack", args);

                AcceptRun = true;
            }




        }





        System.Windows.Forms.WebBrowser WebViewBrowser;
        Window View;
        bool Completeload = false;
        string SelectedIDForWeb = "";


        void WebView()
        {

            if (DT.Rows.Count > 0)
            {
                SelectedIDForWeb = DT.Rows[Datagrid1.Items.IndexOf(Datagrid1.CurrentItem)][3].ToString();

                WebViewBrowser = new System.Windows.Forms.WebBrowser { ScriptErrorsSuppressed = true };

                WebViewBrowser.Navigate(new Uri(Path));

                WebViewBrowser.DocumentCompleted += WebViewBrowser_DocumentCompleted; ;


            }
        }

        private void WebView_Click(object sender, RoutedEventArgs e)
        {

            WebView();
        }

        private void WebViewBrowser_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            if (Completeload == true)
            {
                Completeload = false;
                View = new Window();


                Grid grid = new Grid();


                // Define the Rows
                RowDefinition rowDef1 = new RowDefinition() { Height = new GridLength(50) };
                RowDefinition rowDef2 = new RowDefinition();

                grid.RowDefinitions.Add(rowDef1);
                grid.RowDefinitions.Add(rowDef2);






                System.Windows.Forms.Integration.WindowsFormsHost formsHost = new System.Windows.Forms.Integration.WindowsFormsHost();


                formsHost.Child = WebViewBrowser;




                Button btn = new Button { Content = "طباعة", Name = "BtnPrintWeb", VerticalAlignment = VerticalAlignment.Center };


                btn.Click += Btn_Click;




                Grid.SetRow(btn, 0);
                Grid.SetRow(formsHost, 1);

                grid.Children.Add(btn);

                grid.Children.Add(formsHost);


                //View.ShowInTaskbar = false;

                View.Content = grid;

                View.WindowState = WindowState.Maximized;

                View.Show();

                btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));


                return;
            }

            HTMLDocument dom = (HTMLDocument)WebViewBrowser.Document.DomDocument;
            object ie = dom.getElementById("ContentPlaceHolder1_txtCustomerNo");
            if (ie != null)
            {
                if (ie is IHTMLInputElement)
                {
                    ((IHTMLInputElement)ie).value = SelectedIDForWeb;
                }
            }

            object[] args = { "ctl00$ContentPlaceHolder1$btnGetInvoices2", "", true, "Group2", "", false, false };

            WebViewBrowser.Document.InvokeScript("__doPostBack", args);

            Completeload = true;



        }



        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            WebViewBrowser.ShowPrintDialog() ;
        }

        public static bool Isdouble(string input)
        {
            double test = 0;
            return double.TryParse(input, out test);
        }


        DataTable TablePayment;
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement = "")
        {
            return sWhitespace.Replace(input, replacement);
        }
        public DataTable GetPaidTest(string hmtl = "")
        {

            HtmlDocument doc = new HtmlDocument();

            TablePayment = new DataTable();


            doc.LoadHtml(hmtl);

            var m = doc.DocumentNode.SelectSingleNode("//table[@id='ContentPlaceHolder1_gvInvoices']");

            if (m != null)
            {
                doc.LoadHtml(m.InnerHtml);

                var headers = doc.DocumentNode.SelectNodes("//tr/th");

                foreach (HtmlNode header in headers)

                    if (header.InnerText == "قيمة الفاتورة" || header.InnerText == "القيمة المسددة" || header.InnerText == "القيمة المطلوبة" || header.InnerText == "القيمة المتبقية")
                    {
                        TablePayment.Columns.Add(header.InnerText, typeof(double));
                    }
                    else if (header.InnerText == "شهر الإصدار")
                    {
                        TablePayment.Columns.Add(header.InnerText, typeof(DateTime));
                    }
                    else
                    {
                        TablePayment.Columns.Add(header.InnerText);
                    }  // create columns from th
                       // select rows with td elements 
                foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                    TablePayment.Rows.Add(row.SelectNodes("td").Select(td => ReplaceWhitespace(td.InnerText.Trim())).ToArray());



                TablePayment.Columns.Remove("&nbsp;");

                //TablePayment.Columns.Remove("كمية الاستهلاك المحتسبة");
                //TablePayment.Columns.Remove("القيمة المطلوبة");

                //TablePayment.Columns["شهر الإصدار"].ColumnName = "Date";


                TablePayment.Columns["قيمة الفاتورة"].ColumnName = "BillAmount";
                TablePayment.Columns["شهر الإصدار"].ColumnName = "DateBill";

                TablePayment.Columns["القيمة المسددة"].ColumnName = "PaidAmount";
                TablePayment.Columns["تاريخ التسديد"].ColumnName = "PaidDate";
                TablePayment.Columns["القيمة المتبقية"].ColumnName = "RequiredAmount";
            }




            return TablePayment;
        }

        public DataTable GetUnPaidTest(string hmtl)
        {

            HtmlDocument doc = new HtmlDocument();




            doc.LoadHtml(hmtl);

            var m = doc.DocumentNode.SelectSingleNode("//table[@id='ContentPlaceHolder1_gvUnpayedInvoices']");

            TablePayment = new DataTable();

            if (m != null)
            {
                doc.LoadHtml(m.InnerHtml);

                var headers = doc.DocumentNode.SelectNodes("//tr/th");

                foreach (HtmlNode header in headers)

                    if (header.InnerText == "قيمة الفاتورة" || header.InnerText == "القيمة المطلوبة")
                    {
                        TablePayment.Columns.Add(header.InnerText, typeof(double));
                    }
                    else if (header.InnerText == "شهر الإصدار")
                    {
                        TablePayment.Columns.Add(header.InnerText, typeof(DateTime));
                    }
                    else
                    {
                        TablePayment.Columns.Add(header.InnerText);
                    }  // create columns from th
                       // select rows with td elements 
                foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                    TablePayment.Rows.Add(row.SelectNodes("td").Select(td => ReplaceWhitespace(td.InnerText.Trim())).ToArray());



                TablePayment.Columns.Remove("&nbsp;");

                //TablePayment.Columns.Remove("كمية الاستهلاك المحتسبة");
                //TablePayment.Columns.Remove("القيمة المطلوبة");

                //TablePayment.Columns["قيمة الفاتورة"].ColumnName = "Total";


                TablePayment.Columns["شهر الإصدار"].ColumnName = "DateBill";


                TablePayment.Columns["تاريخ التسديد"].ColumnName = "PaidDate";
                TablePayment.Columns["قيمة الفاتورة"].ColumnName = "BillAmount";
                TablePayment.Columns["القيمة المطلوبة"].ColumnName = "RequiredAmount";


            }

            return TablePayment;
        }

        void HideProgress(int Time, IProgress<int> progress)
        {

            System.Threading.Thread.Sleep(Time * 100);
            progress.Report(0);

        }













        string URL = "";
        public string GenerateURL(string SubscriberNumber, string Month, string Year)
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

        System.ComponentModel.BackgroundWorker BW = new System.ComponentModel.BackgroundWorker();


        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

            gridview.Visibility = Visibility.Visible;

            MovetoRowDataGridByIndex(0);

            AcceptRun = false;

            browser1 = new System.Windows.Forms.WebBrowser { ScriptErrorsSuppressed = true };

            browser1.Navigate(new Uri(Path));


            browser1.DocumentCompleted += Browser1_DocumentCompleted;




        }

        private void BW_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            CL.Pass.DataTable.Rows.Add(txtType.Text, txtTotal.Text);
            gridview.Visibility = Visibility.Collapsed;
            IsEnabled = true;


            DT.Rows.Add(null, null, "مجموع", "", DT.Compute("Sum(PreviousAmount)", ""), DT.Compute("Sum(BillAmount)", ""), DT.Compute("Sum(PaidAmount)", ""), DT.Compute("Sum(TotalAmount)", ""), "", "", "", "", "");

            txtTotal.Text = DT.Compute("Sum(TotalAmount)", "").ToString();


        }

        private void BW_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Datagrid1.ItemsSource = DT.DefaultView;


            txtTotal.Text = DT.Compute("Sum(TotalAmount)", "").ToString();



        }



        void MovetoRowDataGridByIndex(int i)
        {

            this.Dispatcher.Invoke(() =>
            {

                if (!Datagrid1.SelectionUnit.Equals(DataGridSelectionUnit.FullRow))
                    throw new ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.");

                if (i < 0 || i > (Datagrid1.Items.Count - 1))
                    throw new ArgumentException(string.Format("{0} is an invalid row index.", i));

                Datagrid1.SelectedItems.Clear();
                /* set the SelectedItem property */
                object item = Datagrid1.Items[i]; // = Product X
                Datagrid1.SelectedItem = item;

                DataGridRow row = Datagrid1.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                if (row == null)
                {
                    /* bring the data item (Product object) into view
                     * in case it has been virtualized away */
                    Datagrid1.ScrollIntoView(item);
                    row = Datagrid1.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                }
                //TODO: Retrieve and focus a DataGridCell object

            });



        }



        string Month, Year = "";

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();

        }


        void LoadedPrepare()
        {
            List<string> ListMonth = new List<string>();
            ListMonth.Add("1");
            ListMonth.Add("2");
            ListMonth.Add("3");
            ListMonth.Add("4");
            ListMonth.Add("5");
            ListMonth.Add("6");
            ListMonth.Add("7");
            ListMonth.Add("8");
            ListMonth.Add("9");
            ListMonth.Add("10");
            ListMonth.Add("11");
            ListMonth.Add("12");



            txtMonth.ItemsSource = ListMonth;

            List<string> ListYear = new List<string>();
            ListYear.Add("2019");
            ListYear.Add("2020");
            ListYear.Add("2021");
            ListYear.Add("2022");
            ListYear.Add("2023");




            txtYear.ItemsSource = ListYear;


            var DisplayDateQuery = DateTime.Now;

            DisplayDateQuery = DisplayDateQuery.AddMonths(-1);



            string today = DisplayDateQuery.ToString("MM");




            txtMonth.Text = (today.StartsWith("0") ? today.Replace("0", "") : today).ToString();





            txtYear.Text = DisplayDateQuery.Year.ToString();




            txtType.Items.Clear();
            List<SubscriberTypeModel> SubscriberTypes = new List<SubscriberTypeModel>();

            System.Data.DataTable DTTypeSubscriber = new CL.DatabaseQueries().GetTypeSubscriber();


            for (int i = 0; i < DTTypeSubscriber.Rows.Count; i++)
            {
                SubscriberTypes.Add(new SubscriberTypeModel { ID = i + 1, Name = DTTypeSubscriber.Rows[i][0].ToString() });

            }

            txtType.ItemsSource = SubscriberTypes;

            txtType.Text = SubscriberSelected;

            if (txtType.SelectedIndex != -1)
            {
                StartFastSearch();
            }
        }


        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {




                SQLCommand = $"NumberSubscriber like '%{txtSearch.Text}%'";

                if (txtSearch.Text == "*")
                {
                    SQLCommand = $"NoteSubscriber = 'Favorites'";
                }
                else if (txtSearch.Text == "delete")
                {
                    SQLCommand = $"NoteSubscriber = 'delete'";
                }



                Datagrid1.ItemsSource = UpdateList(SQLCommand).DefaultView;



                txtSearch.Text = "";




            }


        }



        private void Cashed_Click(object sender, RoutedEventArgs e)
        {
            string ID = DT.Rows[Datagrid1.SelectedIndex][0].ToString();


            string Note = DT.Rows[Datagrid1.SelectedIndex][4].ToString();


            if (Note == "")

            {
                new CL.DatabaseQueries().Update("delete", ID);
            }
            else if (Note == "delete")

            {
                new CL.DatabaseQueries().Update("false", ID);
            }
            else if (Note == "false")

            {
                new CL.DatabaseQueries().Update("", ID);
            }

            Datagrid1.ItemsSource = UpdateList(SQLCommand).DefaultView;

        }



        CL.ElectricData electricData;
        private void BW_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void ExcelFile_Click(object sender, RoutedEventArgs e)
        {
            new CL.ExportExcel().ExportToExcel(DT);
        }

        private void Datagrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DT.Rows.Count > 0)
            {
                txtSearch.Text = DT.Rows[Datagrid1.SelectedIndex]["NumberSubscriber"].ToString();
                Clipboard.SetText(txtSearch.Text);
            }

        }

        private void MiniReport_Click(object sender, RoutedEventArgs e)
        {
            CL.Pass.DataTable.Rows.Add(txtType.Text, txtTotal.Text);
        }



        void StartFastSearch()
        {
            SQLCommand = $"TypeSubscriber like '{txtType.Text}' AND NoteSubscriber <> 'delete'";

            Datagrid1.ItemsSource = UpdateList(SQLCommand).DefaultView;

            if (BW.IsBusy == false)
            {

                IsEnabled = false;


                Month = txtMonth.Text; Year = txtYear.Text;
                gridview.Visibility = Visibility.Visible;
                BW.RunWorkerAsync();

            }
        }





        private void Transaction_Void(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((MenuItem)e.Source).Uid);

            string Data = "";
            switch (index)
            {
                case 0:
                    Data = "delete";
                    break;
                case 1:
                    Data = "false";
                    break;
                case 2:
                    Data = "";
                    break;
                case 3:

                    Data = "Favorites";
                    break;
            }

            string ID = DT.Rows[Datagrid1.Items.IndexOf(Datagrid1.CurrentItem)][1].ToString();






            new CL.DatabaseQueries().Update(Data, ID);


            Datagrid1.ItemsSource = UpdateList(SQLCommand).DefaultView;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if (DT.Rows.Count > 0)
            {
                var Print = new Layouts.PrintPage(txtType.Text, txtMonth.Text + "/" + txtYear.Text, DT);

                var index = ((Button)e.Source).Uid;

                Print.Owner = MainWindow.Main;

                switch (index)
                {
                    case "PrintPreview":

                        Print.Show();

                        break;

                    case "Printer":
                        Print.Visibility = Visibility.Hidden;

                        Print.Show();



                        Print.Print(Print.MainGrid);

                        Print.Close();

                        break;

                    case "Excel":
                        new CL.ExportExcel().ExportToExcel(DT, true, txtType.Text + " اصدار شهر " + txtMonth.Text + "---" + DateTime.Now.ToString("yyyy MM dd"));
                        break;
                }







            }




            //Print_WPF_Preview(new Layouts.PrintPage(txtType.Text, txtMonth.Text + "/" + txtYear.Text));
        }







        private void CustomReport_Click(object sender, RoutedEventArgs e)
        {
            if (DT.Rows.Count > 0)
            {
                string ID = DT.Rows[Datagrid1.Items.IndexOf(Datagrid1.CurrentItem)][3].ToString();
                string SubName = DT.Rows[Datagrid1.Items.IndexOf(Datagrid1.CurrentItem)][1].ToString();
                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).FrameMain.NavigationService.Navigate(new Layouts.CustomReport(ID, SubName));
                    }
                }
                return;
            }
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).FrameMain.NavigationService.Navigate(new Layouts.CustomReport("", ""));
                }
            }


        }

        public bool FirstRun { get; set; }


        private void txtType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirstRun == true)
            {
                int TabIndex = Layouts.SearchSubscribes.Customtab.SelectedIndex;


                string Selected = ((SubscriberTypeModel)txtType.SelectedItem).Name;


                if (TabIndex != -1)
                {
                    ((TabItem)Layouts.SearchSubscribes.Customtab.Items[TabIndex]).Header = Selected;
                }



                //if (txtType.SelectedItem != null)
                //{
                //    //ComboBoxItem cbi1 = (ComboBoxItem)(sender as ComboBox).SelectedItem;
                //    ComboBoxItem cbi = (ComboBoxItem)txtType.SelectedItem;


                //    
                //}
            }



            FirstRun = true;
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintData_Click(object sender, RoutedEventArgs e)
        {
            WebView();
        }

        private void datagridview1_LoadingRow_1(object sender, DataGridRowEventArgs e)
        {
            // Get the DataRow corresponding to the DataGridRow that is loading.
            System.Data.DataRowView item = e.Row.Item as System.Data.DataRowView;



            try
            {
                if (item != null)
                {
                    System.Data.DataRow row = item.Row;


                    e.Row.Foreground = Brushes.Black;
                    // Access cell values values if needed...
                    var colValue = row["NameSubscriber"];


                    //if (colValue.ToString() == "مجموع")
                    //{

                    //    e.Row.FontWeight = FontWeights.Bold;
                    //    e.Row.FontSize = 20;
                    //}


                    var colColor = row["NoteSubscriber"];
                    //if (string.IsNullOrWhiteSpace(colColor.ToString()))
                    //{

                    //    e.Row.Background = Brushes.Black;
                    //}

                    if (colColor.ToString() == "false")
                    {

                        e.Row.Background = Brushes.Green;
                    }
                    else if (colColor.ToString() == "delete")
                    {

                        e.Row.Background = Brushes.Red;
                    }
                    else if (colColor.ToString() == "")
                    {

                        e.Row.Background = Brushes.WhiteSmoke;
                    }
                    else if (colColor.ToString() == "Favorites")
                    {

                        e.Row.Foreground = Brushes.White;
                        e.Row.Background = Brushes.DodgerBlue;
                    }

                }

            }
            catch (Exception)
            {


            }

        }
    }
}
