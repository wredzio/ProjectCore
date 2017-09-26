using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;

namespace GeneticAlgorithmSchedule.Database.Contexts
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        { }

        public DbSet<Models.School> Schools { get; set; }
        public DbSet<AlgorithmConfig> AlgorithmConfigs { get; set; }
        public DbSet<AlgorithmWithWeakestConfig> AithmWithWeakestConfigs { get; set; }
        public DbSet<StudentsGroup> StudentGroups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseClass> CourseClasses { get; set; }
        public DbSet<StudentGroupCourseClass> StudentGroupCourseClasses { get; set; }
        public DbSet<Available> Availables { get; set; }
        public DbSet<Room> Rooms{ get; set; }
    }
}

