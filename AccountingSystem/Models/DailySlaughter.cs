using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class DailySlaughter
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public Guid IdForTransaction { get; set; }

        public DateTime DateForTransaction { get; set; } = DateTime.Now;


        public int CatId { get; set; }

        public double Qty { get; set; }
        public double Price { get; set; }
        public double Total { get => Qty * Price; }


    }
}
