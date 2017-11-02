using GeneticAlgorithmSchedule.Database.Contexts.Shared;
using GeneticAlgorithmSchedule.Database.Models;
using GeneticAlgorithmSchedule.Database.Models.Schools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Database.Contexts.Schools
{
    public class SchoolContext : BaseDbContext<SchoolContext>
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        { }

        public DbSet<School> Schools { get; set; }
        public DbSet<AlgorithmConfig> AlgorithmConfigs { get; set; }
        public DbSet<AlgorithmWithWeakestConfig> AithmWithWeakestConfigs { get; set; }
        public DbSet<StudentsGroup> StudentGroups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseClass> CourseClasses { get; set; }
        public DbSet<StudentGroupCourseClass> StudentGroupCourseClasses { get; set; }
        public DbSet<Available> Availables { get; set; }
        public DbSet<Room> Rooms{ get; set; }
        public DbSet<ApplicationUserSchool> ApplicationUserSchool { get; set; }
    }
}

