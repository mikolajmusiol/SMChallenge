using Microsoft.EntityFrameworkCore;

namespace DeskBooking.Entities
{
    public class SMCDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }

        public SMCDbContext(DbContextOptions<SMCDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .Property(p => p.BookedDay)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(p => p.Bookings)
                .WithOne(x => x.User);

            //modelBuilder.Entity<Booking>()
            //    .HasOne(x => x.Desk)
            //    .WithOne(x => x.Booking)
            //    .HasForeignKey<Desk>("BookingId");

            modelBuilder.Entity<Desk>()
                .HasMany(x => x.Bookings)
                .WithOne(x => x.Desk);

            modelBuilder.Entity<Location>()
                .HasMany(x => x.Desks)
                .WithOne(x => x.Location)
                .HasForeignKey("LocationId");
        }
    }
}
