using GeneticAlgorithmSchedule.Database.Models.Applications;
using GeneticAlgorithmSchedule.Web.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.ConfigureServices
{
    static public class DataInitializerService
    {
        public static async Task SeedRoles(this RoleManager<ApplicationRole> roleManager)
        {
            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }
        }
    }
}
