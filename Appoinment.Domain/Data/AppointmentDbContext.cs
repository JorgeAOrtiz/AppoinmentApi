using Microsoft.EntityFrameworkCore;
using Appointment.Domain.Models;

namespace Appointment.Domain.Data
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var date = new DateTime(2025, 3, 18);

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin User", Email = "admin@example.com", Password = "admin123" },
                new User { Id = 2, Name = "Regular User", Email = "user@example.com", Password = "user123" }
            );

            // Seed Appointments
            modelBuilder.Entity<Models.Appointment>().HasData(
                new Models.Appointment { Id = 1, UserId = 1, Date = date.AddDays(1), Description = "Admin Appointment", State = AppointmentState.Pending },
                new Models.Appointment { Id = 2, UserId = 2, Date = date.AddDays(2), Description = "User Appointment", State = AppointmentState.Pending }
            );

            // Configure relationships and constraints
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasData(
                            new { UserId = 1, RoleId = 1 },
                            new { UserId = 2, RoleId = 2 }
                        );
                    }
                );

            modelBuilder.Entity<Models.Appointment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId);
        }
    }
}