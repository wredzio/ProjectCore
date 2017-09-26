using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeneticAlgorithmSchedule.Database.Models;

namespace GeneticAlgorithmSchedule.Database.Contexts
{
    public class StudentGroupCourseClassFluentMap : IEntityTypeConfiguration<StudentGroupCourseClass>
    {
        public void Configure(EntityTypeBuilder<StudentGroupCourseClass> builder)
        {
            builder
                .HasKey(o => new {
                    o.CourseClassId,
                    o.StudentGroupId
                });

            builder
                .HasOne(sgcc => sgcc.CourseClass)
                .WithMany(p => p.StudentGroupCourseClasses)
                .HasForeignKey(pt => pt.CourseClassId);

            builder
                .HasOne(pt => pt.StudentsGroup)
                .WithMany(t => t.StudentGroupCourseClasses)
                .HasForeignKey(pt => pt.StudentGroupId);

            builder.Property(o => o.CourseClassId).IsRequired();
            builder.Property(o => o.StudentGroupId).IsRequired();
        }
    }
}
