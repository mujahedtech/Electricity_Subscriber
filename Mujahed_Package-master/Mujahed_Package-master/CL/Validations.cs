using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mujahed_Package.CL
{
    class Validations
    {


        public static bool IsNumeric(string input)
        {
            int test = 0;
            return int.TryParse(input, out test);
        }


        public static bool IsDate(string input)
        {
            DateTime test;
            return DateTime.TryParse(input, out test);
        }


        public static bool IsDouble(string input)
        {
            double test;
            return double.TryParse(input, out test);
        }

        public static bool Isdecimal(string input)
        {
            decimal test;
            return decimal.TryParse(input, out test);
        }
    }
}
