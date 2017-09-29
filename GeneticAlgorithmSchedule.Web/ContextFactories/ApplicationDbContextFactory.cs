using GeneticAlgorithmSchedule.Database.Contexts.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.ContextFactories
{
    public class ApplicationDbContextFactory : DesignTimeDbContextFactory<ApplicationDbContext>
    {
    }
}
