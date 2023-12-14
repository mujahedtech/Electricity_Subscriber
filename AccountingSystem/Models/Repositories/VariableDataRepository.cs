using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    class VariableDataRepository : IDatabaseRepository<VariableDataTbl>
    {
        DbContext db;

        public VariableDataRepository()
        {
            db = new DbContext();


        }
        public Task<int> Add(VariableDataTbl entity)
        {
            return db._database.InsertAsync(entity);
        }

        public Task<int> Delete(VariableDataTbl entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<VariableDataTbl> Find(int id)
        {
            return db._database.Table<VariableDataTbl>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<VariableDataTbl>> List()
        {
            return db._database.Table<VariableDataTbl>().ToListAsync();
        }

        public Task<List<VariableDataTbl>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(VariableDataTbl entity)
        {
            return db._database.UpdateAsync(entity);
        }
    }
}
