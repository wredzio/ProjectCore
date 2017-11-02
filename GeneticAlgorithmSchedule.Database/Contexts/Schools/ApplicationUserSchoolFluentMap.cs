using GeneticAlgorithmSchedule.Database.Models.Applications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeneticAlgorithmSchedule.Database.Models.Schools;

namespace GeneticAlgorithmSchedule.Database.Contexts.Applications
{
    public class ApplicationUserSchoolFluentMap : IEntityTypeConfiguration<ApplicationUserSchool>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserSchool> builder)
        {
            builder
                .HasKey(o => new {
                    o.UserId,
                    o.SchoolId
                });

            builder
                .HasOne(sgcc => sgcc.School)
                .WithMany(p => p.ApplicationUserSchools)
                .HasForeignKey(pt => pt.SchoolId);

            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.SchoolId).IsRequired();
        }
    }
}
