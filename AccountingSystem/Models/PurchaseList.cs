using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class PurchaseList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public Guid IdForTransaction { get; set; }

        public int VendorId { get; set; }
        public int InventoryId { get; set; }

        public DateTime DateEntered { get; set; } = DateTime.Now;


        public int CatId { get; set; }

        public double Qty { get; set; }
        public double Price { get; set; }
        public double Total { get => Qty * Price; }
    }
}
