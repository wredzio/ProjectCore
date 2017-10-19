using GeneticAlgorithmSchedule.Database.Models.Schools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts.Schools
{
    public class StudentsGroupFluentMap : IEntityTypeConfiguration<StudentsGroup>
    {
        public void Configure(EntityTypeBuilder<StudentsGroup> builder)
        {
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.NumberOfStudents).IsRequired();
        }
    }
}

