using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mujahed_Package.CL
{
    class DataBase
    {


        public static void SentEmail()
        {
            string EmailName = "Khirat.mujahedtech@outlook.com";
            string EmailPassword = "C#ProgMujahed1994";
            System.Net.Mail.MailMessage Mail = new MailMessage(EmailName, EmailName, "DataBase",
                $"Backup Database for Khirat at date { DateTime.Now.ToString("dd/MM/yyyy")} and time { DateTime.Now.ToLongTimeString()}");

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential(EmailName, EmailPassword);
            client.EnableSsl = true;
            string namefile = "Khirat_DB.accdb";
            Mail.Attachments.Add(new Attachment(namefile));
            client.Send(Mail);
            System.Windows.MessageBox.Show("Done");





        }
        










        //0  //DTAlter.Columns.Add("IDNumber", typeof(System.Int32));
        // 1 //  DTAlter.Columns.Add("DateSales", typeof(System.DateTime));
        //  2//  DTAlter.Columns.Add("SalesManName", typeof(System.String));
        //  3//  DTAlter.Columns.Add("TotalZakah", typeof(System.Decimal));
        //  4//  DTAlter.Columns.Add("TotalRequiredCash", typeof(System.Decimal));
        //  5//  DTAlter.Columns.Add("TotalCashOnHand", typeof(System.Decimal));
        //  6//  DTAlter.Columns.Add("TotalEndCash", typeof(System.Decimal));
        //  7//  DTAlter.Columns.Add("Cash50", typeof(System.Decimal));
        //  8//  DTAlter.Columns.Add("Cash20", typeof(System.Decimal));
        //  9//  DTAlter.Columns.Add("Cash10", typeof(System.Decimal));
        //  10//  DTAlter.Columns.Add("Cash5", typeof(System.Decimal));
        //  11//  DTAlter.Columns.Add("Cash1", typeof(System.Decimal));
        //  12//  DTAlter.Columns.Add("CashNon", typeof(System.Decimal));
        //  13//  DTAlter.Columns.Add("SalesNote", typeof(System.String));
        //  14//  DTAlter.Columns.Add("ImageCash", typeof(System.String));
        //  15//  DTAlter.Columns.Add("SYSID", typeof(System.String));

    }
}
