using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class TransactionAccounting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //Type mean purchase / expense/debit/recipt
        public int TransType { get; set; }

        public Guid TransId { get; set; }
        public int AccountId { get; set; }


        public DateTime DateEntered { get; set; } = DateTime.Now;

        public double Amount { get; set; }
        public string Note { get; set; }

        public int VendorId { get; set; }
        public int InventoryId { get; set; }//يمكن استخدامه مستودع و مركز كلفة ايضا


    }
}
