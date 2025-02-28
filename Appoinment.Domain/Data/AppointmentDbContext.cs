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

            // Configure relationships and constraints
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany();

            modelBuilder.Entity<Models.Appointment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId);
        }
    }
}