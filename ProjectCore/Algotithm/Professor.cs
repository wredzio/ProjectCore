using System.Collections.Generic;
using System;

namespace ProjectCore.Algotithm
{
    public class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CourseClass> CourseClasses { get; set; }

        public override bool Equals(Object obj) 
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType()) 
                return false;

          Professor professor = (Professor)obj;

          return professor.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}