
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Models
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public List<CourseClass> CourseClasses { get; set; }
        public List<Available> Availables { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Teacher teacher = (Teacher)obj;

            return teacher.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Available : BaseEntity
    {
        public int Slot { get; set; }
    }
}
