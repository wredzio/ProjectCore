using GeneticAlgorithmSchedule.Database.Contexts.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.ContextFactories
{
    public class IdentityDbContextFactory : DesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityDbContextFactory()
        {
            _databaseName = "IdentityConnection";
        }
    }
}
