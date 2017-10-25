﻿// <auto-generated />
using GeneticAlgorithmSchedule.Database.Contexts.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace GeneticAlgorithmSchedule.Database.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("20171024165531_CreateDatabase2")]
    partial class CreateDatabase2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationUserSchool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<int>("ApplicationUserId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

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

                    b.ToTable("Available");
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

                    b.ToTable("Course");
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

                    b.ToTable("CourseClass");
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

                    b.ToTable("Room");
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

                    b.ToTable("School");
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

                    b.ToTable("StudentGroupCourseClass");
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

                    b.ToTable("StudentsGroup");
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

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationUserSchool", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationUser", "ApplicationUser")
                        .WithMany("ApplicationUserSchools")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("GeneticAlgorithmSchedule.Database.Models.Applications.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}