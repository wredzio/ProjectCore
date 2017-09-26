using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts
{
    public class StudentsGroupFluentMap : IEntityTypeConfiguration<StudentsGroup>
    {
        public void Configure(EntityTypeBuilder<StudentsGroup> builder)
        {
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.NumberOfStudents).IsRequired();           
            builder.Property(o => o.ModifiedDate).ValueGeneratedOnAddOrUpdate();
            builder.Property(o => o.AddedDate).ValueGeneratedOnAdd();
        }
    }
}

