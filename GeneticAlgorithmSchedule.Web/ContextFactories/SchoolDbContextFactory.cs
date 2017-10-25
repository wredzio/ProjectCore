using GeneticAlgorithmSchedule.Database.Contexts.Schools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.ContextFactories
{
    public class SchoolDbContextFactory : DesignTimeDbContextFactory<SchoolContext>
    {
        public SchoolDbContextFactory()
        {
            _databaseName = "ScheduleConnection";
        }
    }
}
