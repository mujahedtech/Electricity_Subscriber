using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
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


    public class ViewModelPrint : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name { get; set; }




    }




    /// <summary>
    /// Interaction logic for ManualPrint.xaml
    /// </summary>
    public partial class ManualPrint : UserControl
    {
        public ManualPrint()
        {
            InitializeComponent();

            DataContext = new ViewModelPrint();

            imgLogo.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Resources\Khirat.jpg"));

            Loaded += ManualPrint_Loaded;
        }


        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key==Key.PrintScreen)
            {
                MessageBox.Show("");

            }

            base.OnKeyUp(e);
        }

        private void ManualPrint_Loaded(object sender, RoutedEventArgs e)
        {
            txtNumber.Focus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;

            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                uie.MoveFocus(
                new TraversalRequest(
                FocusNavigationDirection.Next));
            }

           else if (e.Key == Key.F1)
            {
                PrintControl.Visibility = Visibility.Collapsed;

                Print(MainGrid);

                PrintControl.Visibility = Visibility.Visible;
            }
            else if (e.Key == Key.Escape)
            {
                popAdvance.IsOpen = false;
            }

            base.OnKeyDown(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintControl.Visibility = Visibility.Collapsed;

            Print(MainGrid);

            PrintControl.Visibility = Visibility.Visible;


            return;
            try
            {
                PrintControl.Visibility = Visibility.Collapsed;
                
                PrintDialog printDialog = new PrintDialog();

                if (printDialog.ShowDialog() == true)
                {

                    printDialog.PrintVisual(MainGrid, "invoice");
                }



            }
            finally
            {

                PrintControl.Visibility = Visibility.Visible;
            }
        }

        private void txtNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Up)
            {
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(txtDate.Text);
                dt = dt.AddDays(+1);
                txtDate.Text = dt.ToString("yyyy/MM/dd");
            }
            if (e.Key == Key.Down)
            {
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(txtDate.Text);
                dt = dt.AddDays(-1);
                txtDate.Text = dt.ToString("yyyy/MM/dd");
            }
        }

        private void btnAdvance_Click(object sender, RoutedEventArgs e)
        {
            popAdvance.IsOpen = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var obj = (RadioButton)sender;

            txtHeader.Text = obj.Content.ToString();

            switch (obj.Uid)
            {
                case "1":
                    imgLogo.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Resources\Khirat.jpg"));

                    //// Create a BitmapSource  
                    //BitmapImage bitmap = new BitmapImage();
                    //bitmap.BeginInit();
                    //bitmap.UriSource = new Uri(@"Resources\Alkhair.png");
                    //bitmap.EndInit();

                    //imgLogo.Source = bitmap;

                    break;

                case "2":
                    imgLogo.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Resources\Alkhair.png"));
                    break;
            }

        }

        private void FastEnter(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;

            txtNumber.Focus();

            switch (obj.Uid)
            {
                case "1":
                    txtName.Text = "عزام العرقان";

                    txtDescrip.Text = "دفعه من الحساب";

                    break;

                case "2":
                    txtDescrip.Text = "دفعه من الحساب";

                    txtName.Text = "سليمان العرقان";
                    break;
            }
        }






        public void Print(Visual v)
        {
            //System.IO.File.WriteAllText("Printer.dll", txtPrinter.Text);

            System.Windows.FrameworkElement e = v as System.Windows.FrameworkElement;
            if (e == null)
                return;

            PrintDialog pd = new PrintDialog();
            //store original scale
            Transform originalScale = e.LayoutTransform;
            //get selected printer capabilities
            System.Printing.PrintCapabilities capabilities = pd.PrintQueue.GetPrintCapabilities(pd.PrintTicket);


            


            //get scale of the print wrt to screen of WPF visual
            double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / e.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                           e.ActualHeight);



            //Transform the Visual to scale
            e.LayoutTransform = new ScaleTransform(.9, 1);







            //get the size of the printer page
            System.Windows.Size sz = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

            //update the layout of the visual to the printer page size.
            e.Measure(sz);
            e.Arrange(new System.Windows.Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));


            PrintQueue queue = new LocalPrintServer().GetPrintQueue(LocalPrintServer.GetDefaultPrintQueue().FullName);

            pd.PrintQueue = queue;


            //now print the visual to printer to fit on the one page.
            pd.PrintVisual(v, "My Print");

            //apply the original transform.
            e.LayoutTransform = originalScale;

           

        }




    }
}
