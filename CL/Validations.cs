using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_Subscriber.CL
{
    class Validations
    {

        public static bool IsDecimal(string input)
        {
            decimal test;
            return decimal.TryParse(input, out test);
        }



    }
}
