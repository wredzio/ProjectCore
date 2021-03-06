﻿
using GeneticAlgorithmSchedule.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.Models.Schools
{
    public class StudentsGroup : BaseEntity
    {
        public string Name { get; set; }
        public int NumberOfStudents { get; set; }
        public List<StudentGroupCourseClass> StudentGroupCourseClasses { get; set; }
    }
}
