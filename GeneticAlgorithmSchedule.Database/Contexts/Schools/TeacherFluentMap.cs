using GeneticAlgorithmSchedule.Database.Models.Schools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts.Schools
{
    public class TeacherFluentMap : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(o => o.FirstName).IsRequired();
            builder.Property(o => o.LastName).IsRequired();
        }
    }

    public class AvailableFluentMap : IEntityTypeConfiguration<Available>
    {
        public void Configure(EntityTypeBuilder<Available> builder)
        {
            builder.Property(o => o.Slot).IsRequired();
        }
    }
}

