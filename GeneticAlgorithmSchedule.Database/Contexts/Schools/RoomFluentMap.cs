using GeneticAlgorithmSchedule.Database.Models.Schools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts.Schools
{
    public class RoomFluentMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.NumberOfSeats).IsRequired();
            builder.Property(o => o.Lab).IsRequired();
        }
    }
}

