using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Contexts
{
    public class StudentsGroupFluentMap : IEntityTypeConfiguration<StudentsGroup>
    {
        public void Configure(EntityTypeBuilder<StudentsGroup> builder)
        {
        }
    }
}

