﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GeneticAlgorithmSchedule.Database.Models;

namespace GeneticAlgorithmSchedule.Database.School.Models
{
    public class CourseClass : BaseEntity
    {
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        public List<StudentGroupCourseClass> StudentGroupCourseClasses { get; set; }
        public int NumberOfSeats { get; set; }
        public bool RequiresLab { get; set; }
        public int Duration { get; set; }
    }
}