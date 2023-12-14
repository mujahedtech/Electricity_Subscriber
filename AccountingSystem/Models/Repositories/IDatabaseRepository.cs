using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    public interface IDatabaseRepository<TEntity>
    {
        Task<List<TEntity>> List();
        Task<TEntity> Find(int id);
        Task<int> Add(TEntity entity);
        Task<int> Update(TEntity entity);
        Task<int> Delete(TEntity entity);
        Task<List<TEntity>> Search(string term);

    }
}
