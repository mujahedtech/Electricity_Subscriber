using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class GasCylinderInventoryTbl
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int IdAccount { get; set; }
        public int IdInv { get; set; }
        public string Recipient { get; set; }
        public int CylinderCount_Receive { get; set; }//عدد الاسطوانات المستلمة
        public int CylinderCount_Out { get; set; }//عدد الاسطوانات المسلمة
        public string Note { get; set; }
    }
}
