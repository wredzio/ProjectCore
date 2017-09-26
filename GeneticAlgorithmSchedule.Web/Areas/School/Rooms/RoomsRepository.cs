using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeneticAlgorithmSchedule.Models;
using GeneticAlgorithmSchedule.Web.Repositories;
using GeneticAlgorithmSchedule.Database.Contexts;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Rooms
{
    public class RoomsRepository : GenericRepository<Room>, IRoomsRepository
    {
        public RoomsRepository(SchoolContext schoolContext) : base(schoolContext)
        {
        }
    }
}
