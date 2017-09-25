﻿// <auto-generated />
using GeneticAlgorithmSchedule.Infrastructure.Selection;
using GeneticAlgorithmSchedule.Web.Areas.School.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace GeneticAlgorithmSchedule.Web.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.AlgorithmConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CrosoverProbability");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsSoftOn");

                    b.Property<int>("MutationProbability");

                    b.Property<int>("MutationSize");

                    b.Property<int>("NumberOfChromosomes");

                    b.Property<int>("NumberOfCrossoverPoints");

                    b.Property<int>("ReplaceByGeneration");

                    b.Property<int>("SelectionType");

                    b.Property<double>("SoftConditionSufficient");

                    b.Property<int>("TrackBest");

                    b.HasKey("Id");

                    b.ToTable("AlgorithmConfigs");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AlgorithmConfig");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.Available", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Slot");

                    b.Property<int?>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Availables");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.CourseClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CourseId");

                    b.Property<int>("Duration");

                    b.Property<int>("NumberOfSeats");

                    b.Property<bool>("RequiresLab");

                    b.Property<int?>("SchoolId");

                    b.Property<int?>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("SchoolId");

                    b.HasIndex("TeacherId");

                    b.ToTable("CourseClasses");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Lab");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfSeats");

                    b.Property<int?>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NumberOfHoursInDay");

                    b.Property<int>("NumberOfWorkDays");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.StudentGroupCourseClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseClassId");

                    b.Property<int>("StudentGroupId");

                    b.Property<int?>("StudentsGroupId");

                    b.HasKey("Id");

                    b.HasIndex("CourseClassId");

                    b.HasIndex("StudentsGroupId");

                    b.ToTable("StudentGroupCourseClasses");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.StudentsGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfStudents");

                    b.Property<int?>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("StudentGroups");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.AlgorithmWithWeakestConfig", b =>
                {
                    b.HasBaseType("GeneticAlgorithmSchedule.Models.AlgorithmConfig");

                    b.Property<int>("TrackWorst");

                    b.ToTable("AlgorithmWithWeakestConfig");

                    b.HasDiscriminator().HasValue("AlgorithmWithWeakestConfig");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.Available", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Models.Teacher")
                        .WithMany("Availables")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.Course", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Models.School")
                        .WithMany("Courses")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.CourseClass", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("GeneticAlgorithmSchedule.Models.School")
                        .WithMany("CourseClasses")
                        .HasForeignKey("SchoolId");

                    b.HasOne("GeneticAlgorithmSchedule.Models.Teacher", "Teacher")
                        .WithMany("CourseClasses")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.Room", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Models.School")
                        .WithMany("Rooms")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.StudentGroupCourseClass", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Models.CourseClass", "CourseClass")
                        .WithMany("StudentGroupCourseClasses")
                        .HasForeignKey("CourseClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneticAlgorithmSchedule.Models.StudentsGroup", "StudentsGroup")
                        .WithMany("StudentGroupCourseClasses")
                        .HasForeignKey("StudentsGroupId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.StudentsGroup", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Models.School")
                        .WithMany("StudentsGroup")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Models.Teacher", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Models.School")
                        .WithMany("Teachers")
                        .HasForeignKey("SchoolId");
                });
#pragma warning restore 612, 618
        }
    }
}
