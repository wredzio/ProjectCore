using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Services
{
    public interface IService<TEntity>
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetByQuery();
        TEntity Post(TEntity item);
        TEntity Put(TEntity item);
        void Delete(int id);
    }
}
