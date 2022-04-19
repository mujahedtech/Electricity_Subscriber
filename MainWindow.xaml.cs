using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using System.Collections.ObjectModel;
using System.Threading;
using System.Reactive.Linq;

namespace Electricity_Subscriber
{
    public class Orders
    {
        public Guid IDOrder { get; set; }
        public string OrderName { get; set; }

        public DateTime DateTimeOrder { get; set; }

        public string DeviceID { get; set; }


    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //GridPass.Visibility = Visibility.Visible;
            FrameMain.NavigationService.Navigate(new Layouts.Home());


           
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ar-JO");


            CL.Pass.DataTable = new System.Data.DataTable();
            CL.Pass.DataTable.Columns.Add("Type", typeof(string));

            CL.Pass.DataTable.Columns.Add("Amount", typeof(string));


            OrdersMode();

            DataContext = this;

           

        }
        public ObservableCollection<Orders> OrdersList { get; set; } = new ObservableCollection<Orders>();


        Firebase.Database.FirebaseClient firebaseclient = new Firebase.Database.FirebaseClient("https://khiratserv-default-rtdb.asia-southeast1.firebasedatabase.app/");
        void OrdersMode()
        {


            var client = new Firebase.Database.FirebaseClient("https://khiratserv-default-rtdb.asia-southeast1.firebasedatabase.app/");
            var child = client.Child(nameof(Orders));

            var observable = child.AsObservable<Orders>();

            //var collection = firebaseclient.Child(nameof(Orders)).AsObservable<Orders>()
            //    .Where(i=>i.Object.OrderName.Contains("Open"))
            //    .Where(i=>i.Object.DateTimeOrder.ToString("yyyy/MM/dd hh:MM")
            //    .Contains(DateTime.Now.ToString("yyyy/MM/dd hh:MM")))
            //    .Subscribe((dbevent) =>
            //{
            //    if (dbevent.Object != null)
            //    {
            //        Dispatcher.BeginInvoke(new Action(() => {
            //            OrdersList.Add(dbevent.Object);

            //        }));
            //    }
            //});

            var collection = firebaseclient.Child(nameof(Orders)).AsObservable<Orders>()
               .Where(i => i.Object.OrderName.Contains("Open"))
               .Where(i => i.Object.DateTimeOrder>DateTime.Now.AddHours(-1))
               .Subscribe((dbevent) =>
               {
                   if (dbevent.Object != null)
                   {
                       Dispatcher.BeginInvoke(new Action(() => {
                           OrdersList.Add(dbevent.Object);

                       }));
                   }
               });



        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key==Key.F1)
            {
                GridPass.Visibility = Visibility.Visible;
                txtPassWord.Focus();
            }
            base.OnKeyDown(e);
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
                    GridPass.Visibility = Visibility.Visible;
                    txtPassWord.Focus();

                    break;
                case 4:
                    //new CL.Update.UpdateEmail().Upload();
                    break;

                case 5:
                    if (FrameMain.NavigationService.CanGoBack)
                    {
                        FrameMain.NavigationService.GoBack();
                    }

                    break;
            }
        }

        public UserControl usc = null;


        Layouts.SearchSubscribes Search = new Layouts.SearchSubscribes();
        private void btnMainButtons(object sender, RoutedEventArgs e)
        {
            double Width = GridNameButtons.ColumnDefinitions[1].ActualWidth;
            GridCursor.Width = Width;
            int index = int.Parse(((Button)e.Source).Uid);


            GridCursor.Margin = new Thickness(0 + (Width * index), 0, 0, 0);

            UserControl usc = null;

            GridMain.Children.Clear();
            switch (index)
            {
                case 0:
                    FrameMain.NavigationService.Navigate(new Layouts.Home());


                    break;

                case 1:
                    FrameMain.NavigationService.Navigate(new Layouts.MiniReport());



                    break;
                case 2:
                    //Search.AddTab();
                 
                    FrameMain.NavigationService.Navigate(Search);

                    break;
                case 3:
                   
                    System.Data.DataTable DTTypeSubscriber = new CL.DatabaseQueries().GetTypeSubscriber();


                    for (int i = 0; i < DTTypeSubscriber.Rows.Count; i++)
                    {
                        if (DTTypeSubscriber.Rows[i][0].ToString()!="محلات ريش اربد")
                        {
                             Search.AddTab(DTTypeSubscriber.Rows[i][0].ToString());
                        }
                       


                    }

                    FrameMain.NavigationService.Navigate(Search);



                    break;
                case 4:
                    //FrameMain.NavigationService.Navigate(new ReportLayouts.MainReport());

                    break;
                case 5:
                    //FrameMain.NavigationService.Navigate(new Cases.NewCaseJo());
                 
                    break;
                case 6:

                    //FrameMain.NavigationService.Navigate(new ExternalPapers.ManagePapers());

                    break;
                case 7:

                    break;

            }


        }






        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                if (txtPassWord.Password.ToString()=="MujahedTech")
                {
                    GridPass.Visibility = Visibility.Collapsed;
                }
                txtPassWord.Password = "";
            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //CL.ElectricData mujahed = new CL.ElectricData(new Layouts.Show_Subscriber().GenerateURL("1440000093", "9", "2021"));



            //double Bill = double.Parse(mujahed.GetDinarCurrent() + "." + mujahed.GetFilesCurrent());
            //double paid = double.Parse(mujahed.GetPaidAmount());


            //string Data = "Welcome" + Environment.NewLine;
            //Data += $"Your Bill is {Bill.ToString()}" + Environment.NewLine;
            //Data += $"Your paid is {paid.ToString()}" + Environment.NewLine;
            //Data += $"Total is {(Bill-paid)}" + Environment.NewLine;


            //MessageBox.Show(mujahed.GetPerviousAmount());



            //datagridtest.ItemsSource = DT(mujahed.GetPaidAmountTest()).DefaultView;





        }

        DataTable DT(string htmlCode)
        {

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlCode);
            var headers = doc.DocumentNode.SelectNodes("//tr/th");
            DataTable table = new DataTable();
            foreach (HtmlNode header in headers)
                table.Columns.Add(header.InnerText); // create columns from th
                                                     // select rows with td elements 
            foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());


            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i][0] = double.Parse(table.Rows[i][0].ToString()).ToString(); ;
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i][1] = double.Parse(table.Rows[i][1].ToString()).ToString(); ;
            }

            return table;

        }

        private void Languagecombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Languagecombo.SelectedIndex==0)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ar-JO");
                return;
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GridPass.Visibility = Visibility.Collapsed;
        }
    }
}
