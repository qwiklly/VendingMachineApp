using Microsoft.EntityFrameworkCore;
using VendingMachineBackend.Models;

namespace VendingMachineBackend.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<MachineLock> MachineLocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coin>()
                .HasIndex(c => c.Nominal)
                .IsUnique();
        }
    }
}
