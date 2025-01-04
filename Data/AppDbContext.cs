using Apoint_pro.Data.Helpers;
using Apoint_pro.Data.models;
using Microsoft.EntityFrameworkCore;

namespace Apoint_pro.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration config;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
        {
            this.config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }



            modelBuilder.Entity<UserType>().HasData(
                new UserType { Id = 1, descripton = "Admin" },
                new UserType { Id = 2, descripton = "Doctor" },
                new UserType { Id = 3, descripton = "Patient" }
            );

            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.phone).HasMaxLength(20);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.phone).IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User() {Id=1 ,Name ="Ameen" , Email = "Ameen@prime.com", password = HashPasswordhelper.HashPassword("Aa@123"), UserType = 1 }
                , new User() {Id=2, Name = "Rawan", Email = "Rawan@prime.com", password = HashPasswordhelper.HashPassword("Rr@123"), UserType = 1 }
                );
            modelBuilder.Entity<User>().Property(u=> u.UserType).HasDefaultValue(3);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseMySql(config["ConnectionStrings:myCon"], ServerVersion.AutoDetect(config["ConnectionStrings:myCon"]))
                    .UseLazyLoadingProxies();  // Enable Lazy Loading
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<Apointment> Apointments { get; set; }

        public DbSet<Cancellation> Cancellations { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

    }
}
