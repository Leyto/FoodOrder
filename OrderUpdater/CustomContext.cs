using Microsoft.EntityFrameworkCore;
using OrderUpdater.BusinessLogic;

namespace OrderUpdater
{
    public class CustomContext : DbContext
    {
        public CustomContext(DbContextOptions<CustomContext> options) : base(options)
        {
        }

        public CustomContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"Server=localhost\\SQLEXPRESS;Database=OrdersDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
        }


        public DbSet<Order> Orders { get; set; }
    }
}
