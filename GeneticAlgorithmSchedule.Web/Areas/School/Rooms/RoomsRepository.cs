using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeneticAlgorithmSchedule.Web.Repositories;
using GeneticAlgorithmSchedule.Database.Models.Schools;
using GeneticAlgorithmSchedule.Database.Contexts.Schools;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Rooms
{
    public class RoomsRepository : GenericRepository<Room>, IRoomsRepository
    {
        public RoomsRepository(SchoolContext schoolContext) : base(schoolContext)
        {
        }
    }
}
