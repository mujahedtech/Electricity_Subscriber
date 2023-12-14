using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    class DailyTransactionRepository : IDatabaseRepository<DailyTransaction>
    {

        DbContext db;

        public DailyTransactionRepository()
        {
            db = new DbContext();


        }

        public Task<int> Add(DailyTransaction entity)
        {

            return db._database.InsertAsync(entity);
        }

        public Task<int> Delete(DailyTransaction entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<DailyTransaction> Find(int id)
        {
            return db._database.Table<DailyTransaction>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<DailyTransaction>> List()
        {
            return db._database.Table<DailyTransaction>().OrderBy(i => i.Id).ToListAsync();
        }

        public Task<List<DailyTransaction>> ListPerDate(int month, int year)
        {
            DateTime From = new DateTime(year, month, 1);
            DateTime To = new DateTime(year, month, DateTime.DaysInMonth(year, month));


            return db._database.Table<DailyTransaction>().Where(i => i.Date >= From && i.Date <= To).OrderBy(i => i.Date).ToListAsync();
        }


        public Task<List<DailyTransaction>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(DailyTransaction entity)
        {
            return db._database.UpdateAsync(entity);
        }
    }

}
