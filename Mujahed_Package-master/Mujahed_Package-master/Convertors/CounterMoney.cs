using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Mujahed_Package.Convertors
{
    class CounterMoney : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var Text = (TextBox)value;
            if(value.ToString()=="") return "";

            if (value.ToString().Length>0)
            {
                if (CL.Validations.IsDouble(value.ToString()))
                {
                    string text = value.ToString();

                    
                    double? Number = double.Parse(text);
                    if (Number.HasValue)
                    {
                        double MoneyCat = double.Parse(parameter.ToString());

                       

                        return (Number.Value / MoneyCat).ToString("0");
                    }
                }
                
            }

           

            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
