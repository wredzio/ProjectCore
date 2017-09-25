using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticAlgorithmSchedule.Models;
using GeneticAlgorithmSchedule.Web.Repositories;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Repositories.Abstracts
{
    public interface IRoomRepository : IRepository<Room>, IRepositoryAsync<Room>
    {
    }
}
