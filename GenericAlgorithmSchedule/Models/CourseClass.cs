
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GeneticAlgorithmSchedule.Models
{
    public class CourseClass : BaseEntity
    {
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        public List<StudentGroupCourseClass> StudentGroupCourseClasses { get; set; }
        public int NumberOfSeats { get; set; }
        public bool RequiresLab { get; set; }
        public int Duration { get; set; }

        public bool GroupsOverlap(CourseClass courseClass)
        {
            foreach (var group in StudentGroupCourseClasses)
            {
                if (StudentGroupCourseClasses.Any(o => o.CourseClass.Equals(group.CourseClass)))
                    return true;
            }
            return false;
        }

        public bool TeacherOverlaps(CourseClass courseClass)
        {
            return Teacher.Id == courseClass.Teacher.Id;
        }
    }
}
