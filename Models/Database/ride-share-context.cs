using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RideShareCase
{
    public class RideShareContext : DbContext
    {
        public DbSet<TripPlan> TripPlans { get; set; }
        public DbSet<City> Cities { get; set; }
        public readonly IConfiguration _configuration;
        public RideShareContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("sqlConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TripPlan>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<TripPlan>()
                .HasMany<City>(e => e.CitiesOnTheWay)
                .WithMany(x => x.tripPlans);
            modelBuilder.Entity<City>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<City>()
                .HasMany<TripPlan>(e => e.tripPlans)
                .WithMany(x => x.CitiesOnTheWay);
        }

    }
}