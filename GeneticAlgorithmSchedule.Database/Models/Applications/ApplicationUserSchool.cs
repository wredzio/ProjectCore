using GeneticAlgorithmSchedule.Database.Models.Applications;
using GeneticAlgorithmSchedule.Database.Models.Schools;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.Models.Applications
{
    public class ApplicationUserSchool : BaseEntity
    {
        public int SchoolId { get; set; }
        public School School { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
