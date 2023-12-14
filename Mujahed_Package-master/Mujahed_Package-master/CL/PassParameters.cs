using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mujahed_Package.CL
{
    class PassParameters
    {

       public static string DateFormat = "MM/dd/yyyy";

        static string IDSerialV;
        public static string IDSerial
        {
            get
            {
                return IDSerialV;
            }
            set
            {
                IDSerialV = value;
            }
        }

        static string IDSerialMatchV;
        public static string IDSerialMatch
        {
            get
            {
                return IDSerialMatchV;
            }
            set
            {
                IDSerialMatchV = value;
            }
        }
      


        static bool NotificationUnSaveV;
        public static bool NotificationUnSave
        {
            get
            {
                return NotificationUnSaveV;
            }
            set
            {
                NotificationUnSaveV = value;
            }
        }


        static System.Data.DataTable DTReportV;
        public static System.Data.DataTable DTReport
        {
            get
            {
                return DTReportV;
            }
            set
            {
                DTReportV = value;
            }
        }

    }
}
