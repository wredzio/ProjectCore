using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticAlgorithmSchedule.Web.Repositories;
using GeneticAlgorithmSchedule.Database.Models;
using GeneticAlgorithmSchedule.Database.Models.Schools;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Rooms
{
    public interface IRoomsRepository : IGenericRepository<Room>
    {
    }
}
