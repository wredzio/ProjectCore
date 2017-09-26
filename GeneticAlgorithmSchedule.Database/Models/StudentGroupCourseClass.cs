using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.Models
{
    public class StudentGroupCourseClass : BaseEntity
    {
        public int StudentGroupId { get; set; }
        public StudentsGroup StudentsGroup { get; set; }

        public int CourseClassId { get; set; }
        public CourseClass CourseClass { get; set; }

    }
}
