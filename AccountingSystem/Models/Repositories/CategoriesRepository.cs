using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    public class CategoriesRepository : IDatabaseRepository<Categories>
    {
        DbContext db;

        public CategoriesRepository()
        {
            db = new DbContext();


        }

        public Task<int> Add(Categories entity)
        {


            return db._database.InsertAsync(entity);
        }

        public Task<int> Delete(Categories entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<Categories> Find(int id)
        {
            return db._database.Table<Categories>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Categories>> List()
        {
            return db._database.Table<Categories>().ToListAsync();
        }

        public Task<List<Categories>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Categories entity)
        {


            return db._database.UpdateAsync(entity);
        }
    }

}
