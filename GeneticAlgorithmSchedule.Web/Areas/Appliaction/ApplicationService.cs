using GeneticAlgorithmSchedule.Web.Areas.Appliaction.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Areas.Appliaction
{
    public static class ApplicationService
    {
        public static void AddApplicationService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
        }
    }
}
