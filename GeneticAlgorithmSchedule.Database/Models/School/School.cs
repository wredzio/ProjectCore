
using GeneticAlgorithmSchedule.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.School.Models
{
    public class School : BaseEntity
    {
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<CourseClass> CourseClasses { get; set; }
        public IEnumerable<StudentsGroup> StudentsGroup { get; set; }
        public int NumberOfWorkDays { get; set; }
        public int NumberOfHoursInDay { get; set; }
    }
}

