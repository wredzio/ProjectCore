using GeneticAlgorithmSchedule.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts
{
    public class CourseClassFluentMap : IEntityTypeConfiguration<CourseClass>
    {
        public void Configure(EntityTypeBuilder<CourseClass> builder)
        {
            builder.Property(o => o.NumberOfSeats).IsRequired();
            builder.Property(o => o.Duration).IsRequired();
            builder.Property(o => o.StudentGroupCourseClasses).IsRequired();
            builder.Property(o => o.Course).IsRequired();
            builder.Property(o => o.Teacher).IsRequired();
            builder.Property(o => o.RequiresLab).IsRequired();
        }
    }
}

