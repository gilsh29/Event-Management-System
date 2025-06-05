using Microsoft.EntityFrameworkCore;
using EventsData.Models;

namespace EventsData
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options)
            : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EventUser> EventUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite primary key
            modelBuilder.Entity<EventUser>()
                .HasKey(eu => new { eu.EventRef, eu.UserRef });

            // Foreign key to Event with navigation
            modelBuilder.Entity<EventUser>()
                .HasOne(eu => eu.Event)
                .WithMany(e => e.EventUsers)
                .HasForeignKey(eu => eu.EventRef)
                .OnDelete(DeleteBehavior.Cascade);

            // Foreign key to User with navigation
            modelBuilder.Entity<EventUser>()
                .HasOne(eu => eu.User)
                .WithMany(u => u.EventUsers)
                .HasForeignKey(eu => eu.UserRef)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
