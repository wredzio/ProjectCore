using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts
{
    public class TeacherFluentMap : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(o => o.FirstName).IsRequired();
            builder.Property(o => o.LastName).IsRequired();
            builder.Property(o => o.ModifiedDate).ValueGeneratedOnAddOrUpdate();
            builder.Property(o => o.AddedDate).ValueGeneratedOnAdd();
        }
    }

    public class AvailableFluentMap : IEntityTypeConfiguration<Available>
    {
        public void Configure(EntityTypeBuilder<Available> builder)
        {
            builder.Property(o => o.Slot).IsRequired();
            builder.Property(o => o.ModifiedDate).ValueGeneratedOnAddOrUpdate();
            builder.Property(o => o.AddedDate).ValueGeneratedOnAdd();
        }
    }
}

