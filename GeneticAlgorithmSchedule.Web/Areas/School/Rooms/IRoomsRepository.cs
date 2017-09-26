using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticAlgorithmSchedule.Models;
using GeneticAlgorithmSchedule.Web.Repositories;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Rooms
{
    public interface IRoomsRepository : IGenericRepository<Room>
    {
    }
}
