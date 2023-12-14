using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class Inventory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string InvName { get; set; }
        public bool IsGasInv { get; set; }

    }
}
