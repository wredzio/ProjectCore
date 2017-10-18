using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Database.Models.Applications
{
    public class ApplicationUser : IdentityUser<int>
    {
        public IEnumerable<int> AvailableSchoolsId { get; set; }
        public IEnumerable<ApplicationUserSchool> ApplicationUserSchools { get; set; }
    }
}
