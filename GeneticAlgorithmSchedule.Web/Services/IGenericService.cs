using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetByQuery();
        Task<TEntity> Post(TEntity item);
        Task<TEntity> Put(TEntity item);
        Task Delete(int id);
    }
}
