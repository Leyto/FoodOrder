using Microsoft.EntityFrameworkCore;
using OrderUpdater.BusinessLogic;

namespace OrderUpdater
{
    public class CustomContext : DbContext
    {
        public CustomContext(DbContextOptions<CustomContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
        }


        public DbSet<Order> Orders { get; set; }
    }
}
