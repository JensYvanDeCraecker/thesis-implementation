using Microsoft.EntityFrameworkCore;
using ParcelTracker.Models.Database;

namespace ParcelTracker
{
    public class ParcelDbContext : DbContext
    {
        public ParcelDbContext() { }

        public ParcelDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Parcel> Parcels { get; set; }

        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParcelDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}