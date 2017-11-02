using GeneticAlgorithmSchedule.Database.Models.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.Models.Schools
{
    public class ApplicationUserSchool : BaseEntity
    {
        public int SchoolId { get; set; }
        public School School { get; set; }

        public int UserId { get; set; }
    }
}
