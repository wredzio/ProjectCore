using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
        IQueryable<TEntity> GetByQuery();
        Task<TEntity> Post(TEntity item);
        Task<TEntity> Put(TEntity item);
        Task Delete(int id);
    }
}
