using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    public class DailySlaughterRepository : IDatabaseRepository<DailySlaughter>
    {
        DbContext db;

        public DailySlaughterRepository()
        {
            db = new DbContext();


        }

        public Task<int> Add(DailySlaughter entity)
        {
            return db._database.InsertAsync(entity);
        }
        public Task<int> Add(List<DailySlaughter> entity)
        {
            return db._database.InsertAllAsync(entity);
        }

        public Task<int> Delete(DailySlaughter entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<DailySlaughter> Find(int id)
        {
            return db._database.Table<DailySlaughter>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<DailySlaughter>> List()
        {
            return db._database.Table<DailySlaughter>().ToListAsync();
        }
        public Task<List<DailySlaughter>> ListFromIDclass(Guid id)
        {
            return db._database.Table<DailySlaughter>().Where(i => i.IdForTransaction == id).ToListAsync();
        }

        public Task<List<DailySlaughter>> ListPerDate(int month, int year)
        {
            DateTime From = new DateTime(year, month, 1);
            DateTime To = new DateTime(year, month, DateTime.DaysInMonth(year, month));


            return db._database.Table<DailySlaughter>().Where(i => i.DateForTransaction >= From && i.DateForTransaction <= To).OrderBy(i => i.DateForTransaction).ToListAsync();
        }

        public Task<List<DailySlaughter>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(DailySlaughter entity)
        {
            return db._database.UpdateAsync(entity);
        }
    }

}
