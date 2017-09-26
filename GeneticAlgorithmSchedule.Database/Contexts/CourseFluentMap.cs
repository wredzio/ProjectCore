using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts
{
    public class CourseFluentMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.ModifiedDate).ValueGeneratedOnAddOrUpdate();
            builder.Property(o => o.AddedDate).ValueGeneratedOnAdd();
        }
    }
}

