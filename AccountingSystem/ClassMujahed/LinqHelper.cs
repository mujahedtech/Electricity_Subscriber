using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.ClassMujahed
{
    public static class LinqHelper
    {
        public static System.Data.DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new System.Data.DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);



            int Count = 0;


            foreach (var prop in props)
            {
                if (prop.PropertyType.ToString().Contains("System.Collections.Generic.ICollection") == false)
                {

                    tb.Columns.Add(prop.Name, typeof(string));
                    Count++;
                }


            }



            //System.Diagnostics.Process.Start("DataTable.txt");

            foreach (var item in items)
            {
                var values = new object[Count];
                for (var i = 0; i < Count; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
    }
}
