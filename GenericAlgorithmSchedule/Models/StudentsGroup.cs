
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Models
{
    public class StudentsGroup : BaseEntity
    {
        public string Name { get; set; }
        public int NumberOfStudents { get; set; }
        public List<StudentGroupCourseClass> StudentGroupCourseClasses { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            StudentsGroup studentsGroup = (StudentsGroup)obj;

            return studentsGroup.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
