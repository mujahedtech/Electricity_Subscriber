using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_Subscriber.CL
{
    class Electric
    {
        public string direction = "";

        public Electric(string Url)
        {
            WebRequest request = WebRequest.Create(Url);

          

            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                direction = stream.ReadToEnd();
            }

        }
        public string GetFiles()
        {

            string directionIn = direction;

            //Search for the ip in the html
            int first = directionIn.IndexOf("id=\"lblNetAmtF\"");
            int last = directionIn.LastIndexOf("id=\"lblNetAmtD\"");
            directionIn = directionIn.Substring(first, last - first);


            first = directionIn.IndexOf("\"Green\">") + 8;
            last = directionIn.LastIndexOf("</font></b></span>");
            directionIn = directionIn.Substring(first, last - first);


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



        public string GetDinar()
        {
            string directionIn = direction;

            //Search for the ip in the html
            int first = directionIn.IndexOf("id=\"lblNetAmtD\"");
            int last = directionIn.LastIndexOf("id=\"lblOtherDesc9\"");
            directionIn = directionIn.Substring(first, last - first);

           


            first = directionIn.IndexOf("\"Green\">") + 8;
            last = directionIn.LastIndexOf("</font></b></span>");
            directionIn = directionIn.Substring(first, last - first);


            

            return directionIn;
        }
        public string GetPaidDate()
        {
            string directionIn = direction;

            //Search for the ip in the html

            if (direction.Contains("gvPayDet"))
            {
                int first = directionIn.IndexOf("bgcolor=\"#EFF3FB\"")+48;




                directionIn = directionIn.Substring(first, 10);






                System.IO.File.WriteAllText("gvPayDet.txt", directionIn);
            

               

                
            }





          




            return directionIn;
        }

        public bool GetPaidState()
        {
           




            return direction.Contains("gvPayDet");
        }



        public string GetPaidMethod()
        {

            string Data = "غير محدد";
            if (GetPaidState())
            {
                Data = GetPaidMethodName();
            }
            if (GetPaidState()==false) Data = "";


            return Data;
        }




        public string GetPaidMethodName()
        {
            string directionIn = direction;

            //System.IO.File.WriteAllText("tset.txt", "");
            //System.IO.File.WriteAllText("tset.txt", direction);



            //    //Search for the ip in the html
            //    int first = directionIn.IndexOf("bgcolor=\"#EFF3FB\">");
            //    int last = directionIn.LastIndexOf("</table>");
            //    directionIn = directionIn.Substring(first, last - first);




            //first = directionIn.IndexOf("color=\"Green\">")+104;
            //last = directionIn.LastIndexOf("</tr>");
            //directionIn = directionIn.Substring(first, last - first);

            //first = directionIn.IndexOf("color=\"Green\">")+14 ;
            //last = directionIn.LastIndexOf("</font></td>");
            //directionIn = directionIn.Substring(first, last - first);



            directionIn = "";
            if (TablePayment.Rows.Count>0)
            {
                directionIn = TablePayment.Rows[0][3].ToString();
            }



            return directionIn;
        }



        //public string GetPaidAmount()
        //{
        //    string directionIn = direction;

        //    System.IO.File.WriteAllText("tset.txt", "");
        //    System.IO.File.WriteAllText("tset.txt", direction);



        //    if (directionIn.Contains("تسديد آلي"))
        //    {
        //        //Search for the ip in the html
        //        int first = directionIn.IndexOf("bgcolor=\"#EFF3FB\">");
        //        int last = directionIn.LastIndexOf(">تسديد آلي</font></td>");
        //        directionIn = directionIn.Substring(first, last - first);




        //        first = directionIn.IndexOf("nbsp") + 41;
        //        last = directionIn.LastIndexOf("</font></td><td>");
        //        directionIn = directionIn.Substring(first, last - first);
        //    }

        //   else if (directionIn.Contains("ارسالية شيكات"))
        //    {
        //        //Search for the ip in the html
        //        int first = directionIn.IndexOf("bgcolor=\"#EFF3FB\">");
        //        int last = directionIn.LastIndexOf(">ارسالية شيكات</font></td>");
        //        directionIn = directionIn.Substring(first, last - first);




        //        first = directionIn.IndexOf("<td><font color=\"Green\">")+114 ;
        //        last = directionIn.LastIndexOf("</font></td>");
        //        directionIn = directionIn.Substring(first, last - first);
        //    }

        //  else  if (directionIn.Contains(GetPaidMethodName()))
        //    {
        //        //Search for the ip in the html
        //        int first = directionIn.IndexOf("bgcolor=\"#EFF3FB\">");
        //        int last = directionIn.LastIndexOf($">{GetPaidMethodName()}</font></td>");
        //        directionIn = directionIn.Substring(first, last - first);




        //        first = directionIn.IndexOf("<td><font color=\"Green\">") + 114;
        //        last = directionIn.LastIndexOf("</font></td>");
        //        directionIn = directionIn.Substring(first, last - first);
        //    }




        //    return directionIn;
        //}


        DataTable TablePayment = new DataTable();
        public string GetPaidAmount()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(GetPaidAmountTest());
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

        public string GetDinarCurrent()
        {
            string directionIn = direction;

            System.IO.File.WriteAllText("tset.txt", "");
            System.IO.File.WriteAllText("tset.txt", directionIn);

            //Search for the ip in the html
            int first = directionIn.IndexOf("id=\"lblBillTotD\"");
            int last = directionIn.LastIndexOf("id=\"lblOtherDesc7\"");
            directionIn = directionIn.Substring(first, last - first);




            first = directionIn.IndexOf("\"Green\">") + 8;
            last = directionIn.LastIndexOf("</font></b></span>");
            directionIn = directionIn.Substring(first, last - first);


            if (directionIn=="")
            {
                directionIn = "0";
            }


            return directionIn;
        }

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



        public string GetPaidAmountTest()
        {
            string directionIn = direction;

            System.IO.File.WriteAllText("tset.txt", "");
            System.IO.File.WriteAllText("tset.txt", direction);



            //Search for the ip in the html
            int first = directionIn.IndexOf("direction:rtl;float:right;height :20px;");
            int last = directionIn.LastIndexOf("lblQty");
            directionIn = directionIn.Substring(first, last - first);

            first = directionIn.IndexOf("<table cellpadding=");
             last = directionIn.LastIndexOf("</td>");
            directionIn = directionIn.Substring(first, last - first);


            string data = "<table cellpadding=\"0\" cellspacing=\"0\"><tr><th scope=\"col\">Files</th><th scope=\"col\">Dinar</th></tr>";

            directionIn = directionIn.Replace("<table cellpadding=", data);


            return directionIn;
        }



       


    }
}
