﻿// <auto-generated />
using GeneticAlgorithmSchedule.Database.Contexts.Schools;
using GeneticAlgorithmSchedule.Infrastructure.Selection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace GeneticAlgorithmSchedule.Database.Migrations.SchoolMigrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20171102190329_InitialSchool")]
    partial class InitialSchool
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.AlgorithmConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<int>("CrosoverProbability");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedDate");

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

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.ApplicationUserSchool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("SchoolId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("ApplicationUserSchool");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.Available", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("Slot");

                    b.Property<int?>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Availables");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.CourseClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<int?>("CourseId");

                    b.Property<int>("Duration");

                    b.Property<DateTime>("ModifiedDate");

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

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<bool>("Lab");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfSeats");

                    b.Property<int?>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("NumberOfHoursInDay");

                    b.Property<int>("NumberOfWorkDays");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.StudentGroupCourseClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<int>("CourseClassId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("StudentGroupId");

                    b.Property<int?>("StudentsGroupId");

                    b.HasKey("Id");

                    b.HasIndex("CourseClassId");

                    b.HasIndex("StudentsGroupId");

                    b.ToTable("StudentGroupCourseClasses");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.StudentsGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfStudents");

                    b.Property<int?>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("StudentGroups");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int?>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.AlgorithmWithWeakestConfig", b =>
                {
                    b.HasBaseType("GeneticAlgorithmSchedule.Database.Models.Schools.AlgorithmConfig");

                    b.Property<int>("TrackWorst");

                    b.ToTable("AlgorithmWithWeakestConfig");

                    b.HasDiscriminator().HasValue("AlgorithmWithWeakestConfig");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.ApplicationUserSchool", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.School", "School")
                        .WithMany("ApplicationUserSchools")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.Available", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.Teacher")
                        .WithMany("Availables")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.Course", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.School")
                        .WithMany("Courses")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.CourseClass", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.School")
                        .WithMany("CourseClasses")
                        .HasForeignKey("SchoolId");

                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.Teacher", "Teacher")
                        .WithMany("CourseClasses")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.Room", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.School")
                        .WithMany("Rooms")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.StudentGroupCourseClass", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.CourseClass", "CourseClass")
                        .WithMany("StudentGroupCourseClasses")
                        .HasForeignKey("CourseClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.StudentsGroup", "StudentsGroup")
                        .WithMany("StudentGroupCourseClasses")
                        .HasForeignKey("StudentsGroupId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.StudentsGroup", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.School")
                        .WithMany("StudentsGroup")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Schools.Teacher", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Schools.School")
                        .WithMany("Teachers")
                        .HasForeignKey("SchoolId");
                });
#pragma warning restore 612, 618
        }
    }
}