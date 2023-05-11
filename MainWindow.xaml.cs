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


    public class ColorsListClass 
    {

        public int Id { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }

    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public static MainWindow Main;


        void FillColor() 
        {
            List<ColorsListClass> colors = new List<ColorsListClass>();

            colors.Add(new ColorsListClass { Id = 1, ColorName = "Amethyst Purple", ColorCode = "#9966CC" });
            colors.Add(new ColorsListClass { Id = 2, ColorName = "Blue Violet", ColorCode = "#8A2BE2" });
            colors.Add(new ColorsListClass { Id = 3, ColorName = "Chinese Violet", ColorCode = "#856088" });
            colors.Add(new ColorsListClass { Id = 4, ColorName = "Dark Violet", ColorCode = "#9400D3" });
            colors.Add(new ColorsListClass { Id = 5, ColorName = "Electric Indigo", ColorCode = "#6F00FF" });
            colors.Add(new ColorsListClass { Id = 6, ColorName = "Dark Purple", ColorCode = "#551A8B" });
            colors.Add(new ColorsListClass { Id = 7, ColorName = "Electric Purple", ColorCode = "#BF00FF" });
            colors.Add(new ColorsListClass { Id = 8, ColorName = "English Violet", ColorCode = "#563C5C" });
            colors.Add(new ColorsListClass { Id = 9, ColorName = "Heliotrope Purple", ColorCode = "#DF73FF" });
            colors.Add(new ColorsListClass { Id = 10, ColorName = "Indigo Purple", ColorCode = "#4B0082" });
            colors.Add(new ColorsListClass { Id = 11, ColorName = "Lavender Blush", ColorCode = "#FFF0F5" });
            colors.Add(new ColorsListClass { Id = 12, ColorName = "Grape Color", ColorCode = "#6F2DA8" });
            colors.Add(new ColorsListClass { Id = 13, ColorName = "Iris Purple", ColorCode = "#6A5ACD" });
            colors.Add(new ColorsListClass { Id = 14, ColorName = "Byzantium Color", ColorCode = "#702963" });
            colors.Add(new ColorsListClass { Id = 15, ColorName = "African Violet Color", ColorCode = "#B284BE" });
            colors.Add(new ColorsListClass { Id = 16, ColorName = "Prussian Blue Color", ColorCode = "#003153" });
            colors.Add(new ColorsListClass { Id = 17, ColorName = "Navy Blue Color", ColorCode = "#000080" });
            colors.Add(new ColorsListClass { Id = 18, ColorName = "Royal Blue Color", ColorCode = "#4169E1" });
            colors.Add(new ColorsListClass { Id = 19, ColorName = "Midnight Blue Color", ColorCode = "#003366" });
            colors.Add(new ColorsListClass { Id = 20, ColorName = "Turquoise Color", ColorCode = "#40E0D0" });
            colors.Add(new ColorsListClass { Id = 21, ColorName = "Cobalt Blue Color", ColorCode = "#0047AB" });
            colors.Add(new ColorsListClass { Id = 22, ColorName = "Sky Blue Color", ColorCode = "#03BFFF" });
            colors.Add(new ColorsListClass { Id = 23, ColorName = "Electric Blue Color", ColorCode = "#0592D0" });
            colors.Add(new ColorsListClass { Id = 24, ColorName = "Cerulean Blue Color", ColorCode = "#2A52BE" });
            colors.Add(new ColorsListClass { Id = 25, ColorName = "True Blue Color", ColorCode = "#0073CF" });
            colors.Add(new ColorsListClass { Id = 26, ColorName = "Burgundy Color", ColorCode = "#800020" });
            colors.Add(new ColorsListClass { Id = 27, ColorName = "Goldenrod", ColorCode = "#DAA520" });
            colors.Add(new ColorsListClass { Id = 28, ColorName = "Maroon Color", ColorCode = "#800000" });
            colors.Add(new ColorsListClass { Id = 29, ColorName = "Khaki Color", ColorCode = "#BDB76B" });
            colors.Add(new ColorsListClass { Id = 30, ColorName = "Tan Color", ColorCode = "#D2B48C" });
            colors.Add(new ColorsListClass { Id = 31, ColorName = "Taupe Color", ColorCode = "#B38B6D" });
            colors.Add(new ColorsListClass { Id = 32, ColorName = "Bronze Color", ColorCode = "#CD7F32" });
            colors.Add(new ColorsListClass { Id = 33, ColorName = "Copper Color", ColorCode = "#B87333" });
            colors.Add(new ColorsListClass { Id = 34, ColorName = "Burnt Sienna", ColorCode = "#E97451" });
            colors.Add(new ColorsListClass { Id = 35, ColorName = "Chestnut Color", ColorCode = "#954535" });
            colors.Add(new ColorsListClass { Id = 36, ColorName = "Olive Color", ColorCode = "#808000" });
            colors.Add(new ColorsListClass { Id = 37, ColorName = "Sienna Color", ColorCode = "#A0522D" });
            colors.Add(new ColorsListClass { Id = 38, ColorName = "Burnt Umber", ColorCode = "#8A3324" });
            colors.Add(new ColorsListClass { Id = 39, ColorName = "Ecru Color", ColorCode = "#C2B280" });
            colors.Add(new ColorsListClass { Id = 40, ColorName = "Ochre Color", ColorCode = "#CC7722" });
            colors.Add(new ColorsListClass { Id = 41, ColorName = "Lemon Yellow", ColorCode = "#FFF44F" });
            colors.Add(new ColorsListClass { Id = 42, ColorName = "Canary Yellow", ColorCode = "#FFEF00" });
            colors.Add(new ColorsListClass { Id = 43, ColorName = "Dandelion Color", ColorCode = "#F0E130" });
            colors.Add(new ColorsListClass { Id = 44, ColorName = "Citrine Color", ColorCode = "#E4D008" });
            colors.Add(new ColorsListClass { Id = 45, ColorName = "Light Yellow", ColorCode = "#FFFFE0" });
            colors.Add(new ColorsListClass { Id = 46, ColorName = "Flax Color", ColorCode = "#EEDC82" });
            colors.Add(new ColorsListClass { Id = 47, ColorName = "Golden Yellow", ColorCode = "#FFDF00" });
            colors.Add(new ColorsListClass { Id = 48, ColorName = "Yellow Green", ColorCode = "#9ACD32" });
            colors.Add(new ColorsListClass { Id = 49, ColorName = "Mustard Yellow", ColorCode = "#E1AD01" });
            colors.Add(new ColorsListClass { Id = 50, ColorName = "Brass Color", ColorCode = "#B5A642" });
            colors.Add(new ColorsListClass { Id = 51, ColorName = "Saffron Yellow", ColorCode = "#F4C430" });
            colors.Add(new ColorsListClass { Id = 52, ColorName = "Straw Color", ColorCode = "#E4D96F" });
            colors.Add(new ColorsListClass { Id = 53, ColorName = "Beige Color", ColorCode = "#F5F5DC" });
            colors.Add(new ColorsListClass { Id = 54, ColorName = "Chartreuse Color", ColorCode = "#7FFF00" });
            colors.Add(new ColorsListClass { Id = 55, ColorName = "Kelly Green", ColorCode = "#4CBB17" });
            colors.Add(new ColorsListClass { Id = 56, ColorName = "Turquoise", ColorCode = "#40E0D0" });
            colors.Add(new ColorsListClass { Id = 57, ColorName = "Lime Green", ColorCode = "#32CD32" });
            colors.Add(new ColorsListClass { Id = 58, ColorName = "Hunter Green", ColorCode = "#355E3B" });
            colors.Add(new ColorsListClass { Id = 59, ColorName = "Cyan Color", ColorCode = "#00FFFF" });
            colors.Add(new ColorsListClass { Id = 60, ColorName = "Forest Green", ColorCode = "#228B22" });
            colors.Add(new ColorsListClass { Id = 61, ColorName = "Neon Green", ColorCode = "#39FF14" });
            colors.Add(new ColorsListClass { Id = 62, ColorName = "Aquamarine Color", ColorCode = "#7FFFD4" });
            colors.Add(new ColorsListClass { Id = 63, ColorName = "Dark Green", ColorCode = "#006400" });
            colors.Add(new ColorsListClass { Id = 64, ColorName = "Light Green", ColorCode = "#90EE90" });
            colors.Add(new ColorsListClass { Id = 65, ColorName = "Spring Green", ColorCode = "#00FF7F" });
            colors.Add(new ColorsListClass { Id = 66, ColorName = "Mint Color", ColorCode = "#AAF0D1" });
            colors.Add(new ColorsListClass { Id = 67, ColorName = "Pastel Green", ColorCode = "#77DD77" });
            colors.Add(new ColorsListClass { Id = 68, ColorName = "Teal Color", ColorCode = "#008080" });
            colors.Add(new ColorsListClass { Id = 69, ColorName = "Charcoal Color", ColorCode = "#36454F" });
            colors.Add(new ColorsListClass { Id = 70, ColorName = "Ebony Color", ColorCode = "#282C35" });
            colors.Add(new ColorsListClass { Id = 71, ColorName = "Onyx Color", ColorCode = "#0F0F0F" });
            colors.Add(new ColorsListClass { Id = 72, ColorName = "Midnight Blue", ColorCode = "#003366" });
            colors.Add(new ColorsListClass { Id = 73, ColorName = "Black Olive", ColorCode = "#3B3C36" });
            colors.Add(new ColorsListClass { Id = 74, ColorName = "Oxford Blue", ColorCode = "#002147" });
            colors.Add(new ColorsListClass { Id = 75, ColorName = "Jet Color", ColorCode = "#343434" });
            colors.Add(new ColorsListClass { Id = 76, ColorName = "Raisin Color", ColorCode = "#612302" });
            colors.Add(new ColorsListClass { Id = 77, ColorName = "Smoky Black", ColorCode = "#100C08" });
            colors.Add(new ColorsListClass { Id = 78, ColorName = "Bistre Color", ColorCode = "#3D2B1F" });
            colors.Add(new ColorsListClass { Id = 79, ColorName = "Licorice Color", ColorCode = "#1A1110" });
            colors.Add(new ColorsListClass { Id = 80, ColorName = "Eigengrau Color", ColorCode = "#16161D" });
            colors.Add(new ColorsListClass { Id = 81, ColorName = "Black Bean Color", ColorCode = "#3D0C02" });
            colors.Add(new ColorsListClass { Id = 82, ColorName = "Russian Violet", ColorCode = "#32174D" });
            colors.Add(new ColorsListClass { Id = 83, ColorName = "Coral Color", ColorCode = "#F88379" });
            colors.Add(new ColorsListClass { Id = 84, ColorName = "Pink Color", ColorCode = "#FF69B4" });
            colors.Add(new ColorsListClass { Id = 85, ColorName = "Auburn Color", ColorCode = "#922724" });
            colors.Add(new ColorsListClass { Id = 86, ColorName = "Magenta Color", ColorCode = "#FF00FF" });
            colors.Add(new ColorsListClass { Id = 87, ColorName = "Red Color", ColorCode = "#FF0000" });
            colors.Add(new ColorsListClass { Id = 88, ColorName = "Fuchsia Color", ColorCode = "#CA2C92" });
            colors.Add(new ColorsListClass { Id = 89, ColorName = "Dark Red", ColorCode = "#8B0000" });
            colors.Add(new ColorsListClass { Id = 90, ColorName = "Mahogany Color", ColorCode = "#C04000" });
            colors.Add(new ColorsListClass { Id = 91, ColorName = "Blood Red", ColorCode = "#8A0707" });
            colors.Add(new ColorsListClass { Id = 92, ColorName = "Scarlet Color", ColorCode = "#FF2400" });
            colors.Add(new ColorsListClass { Id = 93, ColorName = "Tyrian Purple", ColorCode = "#66023C" });
            colors.Add(new ColorsListClass { Id = 94, ColorName = "Rust Color", ColorCode = "#B7410E" });
            colors.Add(new ColorsListClass { Id = 95, ColorName = "Apricot Color", ColorCode = "#FBCEB1" });
            colors.Add(new ColorsListClass { Id = 96, ColorName = "Neon Orange", ColorCode = "#FF6700" });
            colors.Add(new ColorsListClass { Id = 97, ColorName = "Metallic Gold", ColorCode = "#D4AF37" });
            colors.Add(new ColorsListClass { Id = 98, ColorName = "Buff Color", ColorCode = "#F0DC82" });
            colors.Add(new ColorsListClass { Id = 99, ColorName = "Tawny Color", ColorCode = "#CD5700" });
            colors.Add(new ColorsListClass { Id = 100, ColorName = "Dark Orange", ColorCode = "#EE7600" });
            colors.Add(new ColorsListClass { Id = 101, ColorName = "Pastel Orange", ColorCode = "#FFB347" });
            colors.Add(new ColorsListClass { Id = 102, ColorName = "Tangerine Color", ColorCode = "#F28500" });
            colors.Add(new ColorsListClass { Id = 103, ColorName = "Light Orange", ColorCode = "#FFA500" });
            colors.Add(new ColorsListClass { Id = 104, ColorName = "Bright Orange", ColorCode = "#E24C00" });
            colors.Add(new ColorsListClass { Id = 105, ColorName = "Persimmon Color", ColorCode = "#EC5800" });
            colors.Add(new ColorsListClass { Id = 106, ColorName = "International Orange", ColorCode = "#FF4F00" });
            colors.Add(new ColorsListClass { Id = 107, ColorName = "Orange Red", ColorCode = "#FF4500" });
            colors.Add(new ColorsListClass { Id = 108, ColorName = "Marigold Color", ColorCode = "#EBA832" });
            colors.Add(new ColorsListClass { Id = 109, ColorName = "Safety Orange", ColorCode = "#FF6700" });
            colors.Add(new ColorsListClass { Id = 110, ColorName = "Fuchsia Pink", ColorCode = "#FF77FF" });
            colors.Add(new ColorsListClass { Id = 111, ColorName = "Rouge Color", ColorCode = "#A94064" });
            colors.Add(new ColorsListClass { Id = 112, ColorName = "Flamingo Pink", ColorCode = "#FC8EAC" });
            colors.Add(new ColorsListClass { Id = 113, ColorName = "Dark Pink", ColorCode = "#E75480" });
            colors.Add(new ColorsListClass { Id = 114, ColorName = "Baby Pink", ColorCode = "#F4C2C2" });
            colors.Add(new ColorsListClass { Id = 115, ColorName = "Salmon Color", ColorCode = "#FF9999" });
            colors.Add(new ColorsListClass { Id = 116, ColorName = "Peach Color", ColorCode = "#FFE5B4" });
            colors.Add(new ColorsListClass { Id = 117, ColorName = "Watermelon Color", ColorCode = "#FC6C85" });
            colors.Add(new ColorsListClass { Id = 118, ColorName = "Deep Pink", ColorCode = "#FF1493" });
            colors.Add(new ColorsListClass { Id = 119, ColorName = "Pastel Pink", ColorCode = "#FFD1DC" });
            colors.Add(new ColorsListClass { Id = 120, ColorName = "Light Pink", ColorCode = "#FFB6C1" });
            colors.Add(new ColorsListClass { Id = 121, ColorName = "Rosewood Color", ColorCode = "#65000B" });
            colors.Add(new ColorsListClass { Id = 122, ColorName = "Hot Pink", ColorCode = "#FF69B4" });
            colors.Add(new ColorsListClass { Id = 123, ColorName = "Snow White", ColorCode = "#FFFAFA" });
            colors.Add(new ColorsListClass { Id = 124, ColorName = "Lavender Color", ColorCode = "#E6E6FA" });
            colors.Add(new ColorsListClass { Id = 125, ColorName = "Ivory Color", ColorCode = "#FFFFF0" });
            colors.Add(new ColorsListClass { Id = 126, ColorName = "Blush Color", ColorCode = "#DE5D83" });
            colors.Add(new ColorsListClass { Id = 127, ColorName = "Platinum Color", ColorCode = "#E5E4E2" });
            colors.Add(new ColorsListClass { Id = 128, ColorName = "White Smoke", ColorCode = "#F5F5F5" });
            colors.Add(new ColorsListClass { Id = 129, ColorName = "Navajo White", ColorCode = "#FFDEAD" });
            colors.Add(new ColorsListClass { Id = 130, ColorName = "Linen Color", ColorCode = "#FAF0E6" });
            colors.Add(new ColorsListClass { Id = 131, ColorName = "Magnolia Color", ColorCode = "#F1E1CC" });
            colors.Add(new ColorsListClass { Id = 132, ColorName = "Lemon Color", ColorCode = "#FFF44F" });
            colors.Add(new ColorsListClass { Id = 133, ColorName = "Honeydew Color", ColorCode = "#F0FFF0" });
            colors.Add(new ColorsListClass { Id = 134, ColorName = "Seashell Color", ColorCode = "#FFF5EE" });
            colors.Add(new ColorsListClass { Id = 135, ColorName = "Chiffon Color", ColorCode = "#FFFACD" });
            colors.Add(new ColorsListClass { Id = 136, ColorName = "Silver Color", ColorCode = "#C0C0C0" });
            colors.Add(new ColorsListClass { Id = 137, ColorName = "Slate Gray", ColorCode = "#778899" });
            colors.Add(new ColorsListClass { Id = 138, ColorName = "Gunmetal Gray", ColorCode = "#2C3539" });
            colors.Add(new ColorsListClass { Id = 139, ColorName = "Ash Gray", ColorCode = "#B2BEB5" });
            colors.Add(new ColorsListClass { Id = 140, ColorName = "Purple Gray", ColorCode = "#A7A6BA" });
            colors.Add(new ColorsListClass { Id = 141, ColorName = "Battleship Gray", ColorCode = "#848482" });
            colors.Add(new ColorsListClass { Id = 142, ColorName = "Cool Gray", ColorCode = "#8C92AC" });
            colors.Add(new ColorsListClass { Id = 143, ColorName = "Rose Quartz Color", ColorCode = "#F7CAC9" });
            colors.Add(new ColorsListClass { Id = 144, ColorName = "Payne's Gray", ColorCode = "#40404F" });
            colors.Add(new ColorsListClass { Id = 145, ColorName = "Timberwolf Color", ColorCode = "#DBD7D2" });
            colors.Add(new ColorsListClass { Id = 146, ColorName = "Yellow Gray", ColorCode = "#8F8B66" });



            ListColors.ItemsSource = colors;


        }
        public MainWindow()
        {
            InitializeComponent();
            //GridPass.Visibility = Visibility.Visible;
            FrameMain.NavigationService.Navigate(new Layouts.Home());

            Main = this;


           


            CL.Pass.DataTable = new System.Data.DataTable();
            CL.Pass.DataTable.Columns.Add("Type", typeof(string));

            CL.Pass.DataTable.Columns.Add("Amount", typeof(string));


            OrdersMode();

            DataContext = this;

            Loaded += MainWindow_Loaded;

            FillColor();


        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Languagecombo.SelectedIndex == 0)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ar-JO");

            }
            else if (Languagecombo.SelectedIndex == 1)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            }


           

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
                       if (OrdersList.Where(I=>I.IDOrder== dbevent.Object.IDOrder).Count()==0)
                       {
                           Dispatcher.BeginInvoke(new Action(() => {
                               OrdersList.Add(dbevent.Object);

                           }));
                       }
                     
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
                if (txtPassWord.Password.ToString().ToLower()=="MujahedTech".ToLower())
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
              
            }
           else if (Languagecombo.SelectedIndex == 1)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            }


            //to get selected item text

            //ComboBoxItem typeItem = (ComboBoxItem)Languagecombo.SelectedItem;
            //string value = typeItem.Content.ToString();

            //MessageBox.Show(value);

        }

      


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GridPass.Visibility = Visibility.Collapsed;
        }


        string ColorFile = "Colors.txt";
      

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var Date = button.CommandParameter as ColorsListClass;

            System.IO.File.WriteAllText(ColorFile, Date.ColorCode);

            btnColor.IsChecked = false;

           
        }
    }
}
