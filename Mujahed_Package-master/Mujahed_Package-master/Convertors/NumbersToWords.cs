using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mujahed_Package.Convertors
{
    class NumbersToWords : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Number = value.ToString();
            string data = "";
            if (CL.Validations.Isdecimal(Number))
            {
                try
                {
                    Currency.ToWord toWord = new Currency.ToWord(decimal.Parse(Number), new Currency.CurrencyInfo(Currency.CurrencyInfo.Currencies.Syria));
                    data = toWord.ConvertToArabic();
                }
                catch (Exception)
                {


                }



            }
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
