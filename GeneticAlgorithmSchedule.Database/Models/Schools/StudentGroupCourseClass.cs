using GeneticAlgorithmSchedule.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.Models.Schools
{
    public class StudentGroupCourseClass : BaseEntity
    {
        public int StudentGroupId { get; set; }
        public StudentsGroup StudentsGroup { get; set; }

        public int CourseClassId { get; set; }
        public CourseClass CourseClass { get; set; }
    }
}
