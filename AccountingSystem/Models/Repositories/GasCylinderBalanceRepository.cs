using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    class GasCylinderBalanceRepository : IDatabaseRepository<GasCylinderBalanceTBL>
    {

        DbContext db;

        public GasCylinderBalanceRepository()
        {
            db = new DbContext();


        }

        public Task<int> Add(GasCylinderBalanceTBL entity)
        {
            return db._database.InsertAsync(entity);
        }

        public Task<int> Delete(GasCylinderBalanceTBL entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<GasCylinderBalanceTBL> Find(int id)
        {
            return db._database.Table<GasCylinderBalanceTBL>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<GasCylinderBalanceTBL>> List()
        {
            return db._database.Table<GasCylinderBalanceTBL>().ToListAsync();
        }

        public Task<List<GasCylinderBalanceTBL>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(GasCylinderBalanceTBL entity)
        {
            return db._database.UpdateAsync(entity);
        }
    }

}
