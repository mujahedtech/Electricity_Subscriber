using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class Categories
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CatName { get; set; }
    }
}
