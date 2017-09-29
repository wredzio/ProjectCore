using GeneticAlgorithmSchedule.Database.Models;
using GeneticAlgorithmSchedule.Database.Models.Schools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Database.Contexts.Schools
{
    public class SchoolContext : DbContext
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


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.DetectChanges();
            var now = DateTime.UtcNow;

            foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added))
            {
                item.Property("AddedDate").CurrentValue = now;
                item.Property("ModifiedDate").CurrentValue = now;
            }

            foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Modified))
            {
                item.Property("ModifiedDate").CurrentValue = now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

