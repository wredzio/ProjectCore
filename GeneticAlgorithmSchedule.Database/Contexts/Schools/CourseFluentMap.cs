using GeneticAlgorithmSchedule.Database.Models.Schools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts.Schools
{
    public class CourseFluentMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(o => o.Name).IsRequired();
        }
    }
}

