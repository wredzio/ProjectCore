using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts
{
    public class RoomFluentMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.NumberOfSeats).IsRequired();
            builder.Property(o => o.Lab).IsRequired();
            builder.Property(o => o.ModifiedDate).ValueGeneratedOnAddOrUpdate();
            builder.Property(o => o.AddedDate).ValueGeneratedOnAdd();
        }
    }
}

