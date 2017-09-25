using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetByQuery();
        TEntity Post(TEntity item);
        TEntity Put(TEntity item);
        void Delete(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetByQueryAsync();
        Task<TEntity> PostAsync(TEntity item);
        Task<TEntity> PutAsync(TEntity item);
        Task DeleteAsync(int id);
    }
}
