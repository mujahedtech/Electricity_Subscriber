using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Electricity_Subscriber.Layouts
{
    /// <summary>
    /// Interaction logic for SearchSubscribes.xaml
    /// </summary>
    public partial class SearchSubscribes : UserControl
    {

        public static SearchSubscribes Instance;
        public SearchSubscribes()
        {
            InitializeComponent();

            Instance = this;

            Customtab = MainTab;
        }




        public static TabControl Customtab;


      public  List<Show_Subscriber> Customtabs=new List<Show_Subscriber>();

        public void AddTab(string tabName="")
        {
            TabItem Tab = new TabItem();

            Customtabs.Add(new Layouts.Show_Subscriber(tabName));


            Tab.Content = Customtabs.LastOrDefault();


            if (tabName=="")
            {
                tabName = "Tab " + (MainTab.Items.Count+1);

                var viewcontent = Customtabs.LastOrDefault();

                viewcontent.FirstRun = true;

                Tab.Content = viewcontent;

            }
            Tab.Header = tabName;

            MainTab.Items.Add(Tab);



            MainTab.SelectedIndex = MainTab.Items.Count-1;
            //MainTab.Items.MoveCurrentToLast();




          


        }

        private void MainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //كود لاحقا من اجل معرفة كيفية تعديل اسم التاب
            //TabControl myTabControl = (TabControl)sender;
       


            //((TabItem)myTabControl.SelectedItem).Header =" ";
        
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Selected = Customtab.SelectedIndex;

                Customtab.Items.RemoveAt(Selected);


                Customtabs.RemoveAt(Selected);


                if (Customtab.Items.Count == 0)
                {
                    AddTab();
                    Customtab.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {

                
            }
          
          
        }

       

        private void AddNewTab_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            AddTab();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            
            if (MainTab.Items.Count==1)
            {

                AddNewTab.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
