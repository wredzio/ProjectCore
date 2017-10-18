using GeneticAlgorithmSchedule.Database.Models.Applications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts.Applications
{
    public class ApplicationUserSchoolFluentMap : IEntityTypeConfiguration<ApplicationUserSchool>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserSchool> builder)
        {
            builder
                .HasKey(o => new {
                    o.ApplicationUserId,
                    o.SchoolId
                });

            builder
                .HasOne(sgcc => sgcc.School)
                .WithMany(p => p.ApplicationUserSchools)
                .HasForeignKey(pt => pt.SchoolId);

            builder
                .HasOne(pt => pt.ApplicationUser)
                .WithMany(t => t.ApplicationUserSchools)
                .HasForeignKey(pt => pt.ApplicationUserId);

            builder.Property(o => o.ApplicationUserId).IsRequired();
            builder.Property(o => o.SchoolId).IsRequired();
        }
    }
}
