using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Mujahed_Package
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            if (System.IO.File.Exists(CL.Layer_Data.Path) == false)
            {
                System.IO.File.WriteAllText(CL.Layer_Data.Path, "Khirat_DB.accdb");

            }

            base.OnStartup(e);
        }

    }
}
