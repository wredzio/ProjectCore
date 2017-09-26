using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.Property(o => o.ModifiedDate).ValueGeneratedOnAddOrUpdate();
            builder.Property(o => o.AddedDate).ValueGeneratedOnAdd();
        }
    }
}
