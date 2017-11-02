using GeneticAlgorithmSchedule.Web.Areas.School.Rooms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.ConfigureServices
{
    static public class SchoolService
    {
        public static void AddSchoolService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRoomRepository, RoomRepository>();
            serviceCollection.AddScoped<IRoomService, RoomService>();
        }
    }
}
