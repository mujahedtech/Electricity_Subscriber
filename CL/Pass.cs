using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_Subscriber.CL
{
  public class Pass
    {

        //متغير الفائدة منه هو حفظ من اجل اسم التاب ان يكون متغير
        public static string SubscripeType { get; set; }


       static System.Data.DataTable dt;

        public static System.Data.DataTable DataTable 
        {
            get 
            {
                return dt;
            }

            set 
            {
                dt = value;


            }

        
        }


        static System.Data.DataTable dtprint;

        public static System.Data.DataTable DataTablePrint
        {
            get
            {
                return dtprint;
            }

            set
            {
                dtprint = value;


            }


        }

    }
}
