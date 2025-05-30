﻿// <auto-generated />
using System;
using Apoint_pro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Apoint_pro.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250103235708_addAdmins")]
    partial class addAdmins
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Apoint_pro.Data.models.Apointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("UserId");

                    b.ToTable("Apointments");
                });

            modelBuilder.Entity("Apoint_pro.Data.models.Cancellation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApointmentId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext");

                    b.Property<int>("canceledBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApointmentId");

                    b.HasIndex("canceledBy");

                    b.ToTable("Cancellations");
                });

            modelBuilder.Entity("Apoint_pro.Data.models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("speciality")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("timeFrom")
                        .HasColumnType("time(6)");

                    b.Property<TimeSpan>("timeTo")
                        .HasColumnType("time(6)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Apoint_pro.Data.models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("phone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserType");

                    b.HasIndex("phone")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Ameen@prime.com",
                            Name = "Ameen",
                            UserType = 1,
                            password = "XgWlMnf3Fnz9zLef914Ntb6AdeuQe8fB8IEFSa0zCjk1Hczf"
                        },
                        new
                        {
                            Id = 2,
                            Email = "Rawan@prime.com",
                            Name = "Rawan",
                            UserType = 1,
                            password = "Dqy2jPp8AndovDJUtGLiAL2UHLMWzcNS89xBC5ScTTawtFZm"
                        });
                });

            modelBuilder.Entity("Apoint_pro.Data.models.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("descripton")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            descripton = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            descripton = "Doctor"
                        },
                        new
                        {
                            Id = 3,
                            descripton = "Patient"
                        });
                });

            modelBuilder.Entity("Apoint_pro.Data.models.Apointment", b =>
                {
                    b.HasOne("Apoint_pro.Data.models.Doctor", "doctor")
                        .WithMany("apointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Apoint_pro.Data.models.User", "user")
                        .WithMany("apointments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("doctor");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Apoint_pro.Data.models.Cancellation", b =>
                {
                    b.HasOne("Apoint_pro.Data.models.Apointment", "apointment")
                        .WithMany()
                        .HasForeignKey("ApointmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Apoint_pro.Data.models.User", "user")
                        .WithMany("cancellations")
                        .HasForeignKey("canceledBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("apointment");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Apoint_pro.Data.models.Doctor", b =>
                {
                    b.HasOne("Apoint_pro.Data.models.User", "user")
                        .WithOne("doctor")
                        .HasForeignKey("Apoint_pro.Data.models.Doctor", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Apoint_pro.Data.models.User", b =>
                {
                    b.HasOne("Apoint_pro.Data.models.UserType", "userType")
                        .WithMany("users")
                        .HasForeignKey("UserType")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("userType");
                });

            modelBuilder.Entity("Apoint_pro.Data.models.Doctor", b =>
                {
                    b.Navigation("apointments");
                });

            modelBuilder.Entity("Apoint_pro.Data.models.User", b =>
                {
                    b.Navigation("apointments");

                    b.Navigation("cancellations");

                    b.Navigation("doctor");
                });

            modelBuilder.Entity("Apoint_pro.Data.models.UserType", b =>
                {
                    b.Navigation("users");
                });
#pragma warning restore 612, 618
        }
    }
}
