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
        public SearchSubscribes()
        {
            InitializeComponent();

            Customtab = MainTab;
        }




        public static TabControl Customtab;

        public void AddTab(string tabName="")
        {
            TabItem Tab = new TabItem();

            Tab.Content = new Layouts.Show_Subscriber(tabName);


            if (tabName=="")
            {
                tabName = "Tab " + (MainTab.Items.Count+1);

                var viewcontent = new Layouts.Show_Subscriber(tabName);

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
          
            int Selected = Customtab.SelectedIndex;

            Customtab.Items.RemoveAt(Selected);

            if (Customtab.Items.Count == 0)
            {
                AddTab();
                Customtab.SelectedIndex = 0;
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
