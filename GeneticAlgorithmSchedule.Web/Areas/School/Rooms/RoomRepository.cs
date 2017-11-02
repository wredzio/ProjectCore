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
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(SchoolContext schoolContext) : base(schoolContext)
        {
        }
    }
}
