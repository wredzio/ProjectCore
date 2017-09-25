using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Contexts
{
    public class RoomFluentMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
        }
    }
}

