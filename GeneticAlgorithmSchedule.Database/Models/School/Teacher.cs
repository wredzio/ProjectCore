
using GeneticAlgorithmSchedule.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.School.Models
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CourseClass> CourseClasses { get; set; }
        public List<Available> Availables { get; set; }
    }

    public class Available : BaseEntity
    {
        public int Slot { get; set; }
    }
}
