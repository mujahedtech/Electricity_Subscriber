using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    public class PurchaseListRepository : IDatabaseRepository<PurchaseList>
    {
        DbContext db;

        public PurchaseListRepository()
        {
            db = new DbContext();


        }

        public Task<int> Add(PurchaseList entity)
        {
            return db._database.InsertAsync(entity);
        }
        public Task<int> Add(List<PurchaseList> entity)
        {
            return db._database.InsertAllAsync(entity);
        }

        public Task<int> Delete(PurchaseList entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<PurchaseList> Find(int id)
        {
            return db._database.Table<PurchaseList>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<PurchaseList>> List()
        {
            return db._database.Table<PurchaseList>().ToListAsync();
        }
        public Task<List<PurchaseList>> ListFromIDTrans(Guid id)
        {
            return db._database.Table<PurchaseList>().Where(i => i.IdForTransaction == id).ToListAsync();
        }

        public Task<List<PurchaseList>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(PurchaseList entity)
        {
            return db._database.UpdateAsync(entity);
        }
    }

}
