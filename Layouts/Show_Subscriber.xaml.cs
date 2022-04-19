using Electricity_Subscriber.CL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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



        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {



            SQLCommand = $"TypeSubscriber like '{txtType.Text}' AND NoteSubscriber <> 'delete'";

            Datagrid1.ItemsSource = UpdateList(SQLCommand).DefaultView;


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

        CL.Electric mujahed;
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (BW.IsBusy == false)
            {


                IsEnabled = false;






                Month = txtMonth.Text; Year = txtYear.Text;


                gridview.Visibility = Visibility.Visible;


                BW.RunWorkerAsync();




            }





        }

        private void BW_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            CL.Pass.DataTable.Rows.Add(txtType.Text, txtTotal.Text);
            gridview.Visibility = Visibility.Collapsed;
            IsEnabled = true;


            DT.Rows.Add(null, null, "مجموع","", DT.Compute("Sum(PreviousAmount)", ""), DT.Compute("Sum(BillAmount)", ""), DT.Compute("Sum(PaidAmount)", ""), DT.Compute("Sum(TotalAmount)", ""), "", "", "", "", "");

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

            DisplayDateQuery = DisplayDateQuery.AddMonths(-2);



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
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DT.Rows[i]["TotalAmount"] = 0;

                if (DT.Rows[i]["NoteSubscriber"].ToString() != "false" && DT.Rows[i]["NoteSubscriber"].ToString() != "delete")
                {
                    electricData = new CL.ElectricData(GenerateURL(DT.Rows[i]["NumberSubscriber"].ToString(), Month, Year));

                    DT.Rows[i]["BillAmount"] = electricData.BillAmount;

                    DT.Rows[i]["PaidAmount"] = electricData.PaidAmount;
                    DT.Rows[i]["TotalAmount"] = electricData.TotalAmount;
                    DT.Rows[i]["SubscriberPaid"] = electricData.PaymentNote;


                    DT.Rows[i]["PaidMethod"] = electricData.PaymentMethod;


                    DT.Rows[i]["PreviousAmount"] = electricData.PreviousAmount;


                    if (false)
                    {
                        double Bill = double.Parse(mujahed.GetDinarCurrent() + "." + mujahed.GetFilesCurrent());
                        double paid = 0;


                        DT.Rows[i]["BillAmount"] = Bill;

                        DT.Rows[i]["PaidAmount"] = 0;
                        DT.Rows[i]["TotalAmount"] = 0;
                        DT.Rows[i]["SubscriberPaid"] = "لا يوجد فاتورة";

                        if (Bill != 0)
                        {
                            DT.Rows[i]["SubscriberPaid"] = "غير مدفوع";
                        }


                        DT.Rows[i]["PaidMethod"] = mujahed.GetPaidMethod();



                        if (mujahed.GetPaidState())
                        {
                            paid = double.Parse(mujahed.GetPaidAmount());
                            DT.Rows[i]["PaidAmount"] = paid;
                            DT.Rows[i]["TotalAmount"] = Bill - paid;
                            DT.Rows[i]["SubscriberPaid"] = "مدفوع" + "--" + mujahed.GetPaidDate();
                        }
                        DT.Rows[i]["TotalAmount"] = Bill - paid;

                        string Cost = mujahed.GetDinar().ToString() + "." + mujahed.GetFiles().ToString();
                    }




                    MovetoRowDataGridByIndex(i);

                    BW.ReportProgress(i);



                }







            }
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

        private void fastSearch_Click(object sender, RoutedEventArgs e)
        {

            StartFastSearch();

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
            if (DT.Rows.Count>0)
            {
               

                new Layouts.PrintPage(txtType.Text, txtMonth.Text + "/" + txtYear.Text, DT).Show();
            }
           



            //Print_WPF_Preview(new Layouts.PrintPage(txtType.Text, txtMonth.Text + "/" + txtYear.Text));
        }







        private void CustomReport_Click(object sender, RoutedEventArgs e)
        {
            if (DT.Rows.Count > 0)
            {
                string ID = DT.Rows[Datagrid1.Items.IndexOf(Datagrid1.CurrentItem)][2].ToString();
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



        private void datagridview1_LoadingRow_1(object sender, DataGridRowEventArgs e)
        {
            // Get the DataRow corresponding to the DataGridRow that is loading.
            System.Data.DataRowView item = e.Row.Item as System.Data.DataRowView;

            if (item != null)
            {
                System.Data.DataRow row = item.Row;
                // Access cell values values if needed...
                var colValue = row["NoteSubscriber"];

                if (colValue.ToString() == "false")
                {

                    e.Row.Background = Brushes.Green;
                }
                else if (colValue.ToString() == "delete")
                {

                    e.Row.Background = Brushes.Red;
                }
                else if (colValue.ToString() == "")
                {

                    e.Row.Background = Brushes.WhiteSmoke;
                }
                else if (colValue.ToString() == "Favorites")
                {

                    e.Row.Foreground = Brushes.White;
                    e.Row.Background = Brushes.DodgerBlue;
                }

            }
        }
    }
}
