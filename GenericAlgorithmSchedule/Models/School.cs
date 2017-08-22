
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Models
{
    public class School
    {
        public int Id { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<CourseClass> CourseClasses { get; set; }
        public int NumberOfWorkDays { get; set; }
        public int NumberOfHoursInDay { get; set; }
    }
}

