using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Electricity_Subscriber
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string ColorFile = "Colors.txt";
        protected override void OnStartup(StartupEventArgs e)
        {
           

            if (System.IO.File.Exists(ColorFile)==false) System.IO.File.WriteAllText(ColorFile,"Red");

           string sysColor= System.IO.File.ReadAllText(ColorFile);

            App.Current.Resources["MainColor"] =new SolidColorBrush( (Color)ColorConverter.ConvertFromString(sysColor));

            base.OnStartup(e);  
        }



    }
}
