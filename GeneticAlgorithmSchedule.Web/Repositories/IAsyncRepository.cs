using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Repositories
{
    public interface IAsyncRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetByQueryAsync();
        Task<TEntity> PostAsync(TEntity item);
        Task<TEntity> PutAsync(TEntity item);
        Task DeleteAsync(int id);
    }
}
