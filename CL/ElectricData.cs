

using HtmlAgilityPack;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;

namespace Electricity_Subscriber.CL
{
    class ElectricData
    {

         string direction = "";


        public string PreviousAmount="0", BillAmount="0", PaidAmount="0",TotalAmount="0", PaymentMethod, PaymentDate, PaymentNote = "لا يوجد فاتورة";

        public ElectricData(string Url)
        {
            WebRequest request = WebRequest.Create(Url);
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                direction = stream.ReadToEnd();
            }






            double Bill = double.Parse(GetDinarCurrent() + "." + GetFilesCurrent());

            if (Bill != 0)
            {
                BillAmount = Bill.ToString();
                Bill += double.Parse(GetPerviousAmount());
                PaymentNote = "غير مدفوع";
                PreviousAmount = GetPerviousAmount();

                TotalAmount = Bill.ToString(); ;

            }
            if (GetPaidState())
            {
                PaidAmount = GetPaidAmount();
                PaymentMethod = TablePayment.Rows[0][3].ToString();

                TotalAmount = (Bill - double.Parse(PaidAmount)).ToString();


                PaymentDate = TablePayment.Rows[0][0].ToString();

                PaymentNote =/* "مدفوع"+"--"+*/ PaymentDate;
            }


        }


        //التاكد من انه يوجد دفعه على الفاتورة
        public bool GetPaidState()
        {
            return direction.Contains("gvPayDet");
        }

        //قيمة الفاتورة دينار
        public string GetDinarCurrent()
        {
            string directionIn = direction;

           
            //Search for the ip in the html
            int first = directionIn.IndexOf("id=\"lblBillTotD\"");
            int last = directionIn.LastIndexOf("id=\"lblOtherDesc7\"");
            directionIn = directionIn.Substring(first, last - first);




            first = directionIn.IndexOf("\"Green\">") + 8;
            last = directionIn.LastIndexOf("</font></b></span>");
            directionIn = directionIn.Substring(first, last - first);


            if (directionIn == "")
            {
                directionIn = "0";
            }


            return directionIn;
        }


        //قيمة الفاتورة فلسات
        public string GetFilesCurrent()
        {

            string directionIn = direction;

            //Search for the ip in the html
            int first = directionIn.IndexOf("id=\"lblBillTotF\"");
            int last = directionIn.LastIndexOf("id=\"lblBillTotD\"");
            directionIn = directionIn.Substring(first, last - first);


            first = directionIn.IndexOf("\"Green\">") + 8;
            last = directionIn.LastIndexOf("</font></b></span>");
            directionIn = directionIn.Substring(first, last - first);

            if (directionIn == "")
            {
                directionIn = "0";
            }

            if (directionIn.Length == 1)
            {
                directionIn = "00" + directionIn;
            }
            if (directionIn.Length == 2)
            {
                directionIn = "0" + directionIn;
            }




            return directionIn;
        }


        // يتم من خلال جلب الجدول الخاص بالدفعات
        public string GetPaymentTableHTML()
        {
            string directionIn = direction;


            WriteCode(directionIn);


            //Search for the ip in the html
            int first = directionIn.IndexOf("table class=\"LongText\"");
            int last = directionIn.LastIndexOf("</table>");
            directionIn = directionIn.Substring(first, last - first);



            first = directionIn.IndexOf("table class=\"LongText\"");
            last = directionIn.LastIndexOf("</div>");
            directionIn = directionIn.Substring(first, last - first);



            return directionIn;
        }


        //جدول يتم ادخال فيه بيانات الدفعات
        DataTable TablePayment = new DataTable();

        //قيمة الفاتورة المدفوعة
        public string GetPaidAmount()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(GetPaymentTableHTML());
            var headers = doc.DocumentNode.SelectNodes("//tr/th");

            foreach (HtmlNode header in headers)
                TablePayment.Columns.Add(header.InnerText); // create columns from th
                                                            // select rows with td elements 
            foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                TablePayment.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());

            decimal counter = 0;
            for (int i = 0; i < TablePayment.Rows.Count; i++)
            {
                counter += decimal.Parse(TablePayment.Rows[i][2].ToString());
            }

            return counter.ToString();
        }


        public string GetPerviousAmount()
        {
            string directionIn = direction;

            WriteCode(directionIn);



            //Search for the ip in the html
            int first = directionIn.IndexOf("direction:rtl;float:right;height :20px;");
            int last = directionIn.LastIndexOf("lblQty");
            directionIn = directionIn.Substring(first, last - first);

            first = directionIn.IndexOf("<table cellpadding=");
            last = directionIn.LastIndexOf("</td>");
            directionIn = directionIn.Substring(first, last - first);


            string data = "<table cellpadding=\"0\" cellspacing=\"0\"><tr><th scope=\"col\">Files</th><th scope=\"col\">Dinar</th></tr>";

            directionIn = directionIn.Replace("<table cellpadding=", data);





            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(directionIn);
            var headers = doc.DocumentNode.SelectNodes("//tr/th");
            DataTable table = new DataTable();
            foreach (HtmlNode header in headers)
                table.Columns.Add(header.InnerText); // create columns from th
                                                     // select rows with td elements 
            foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());


           
                 


          
            return double.Parse(table.Rows[0][1].ToString()).ToString() + "." + double.Parse(table.Rows[0][0].ToString()).ToString();
        }



        void WriteCode(string Code)
        {
            try
            {
                System.IO.File.WriteAllText("tset.txt", "");
                System.IO.File.WriteAllText("tset.txt", Code);
            }
            catch (System.Exception)
            {

                
            }
           

        }


    }
}
