using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Contexts
{
    public class TeacherFluentMap : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
        }
    }

    public class AvailableFluentMap : IEntityTypeConfiguration<Available>
    {
        public void Configure(EntityTypeBuilder<Available> builder)
        {
        }
    }
}

