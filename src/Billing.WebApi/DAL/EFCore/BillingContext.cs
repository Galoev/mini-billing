using Microsoft.EntityFrameworkCore;
using Billing.WebApi.Models;

namespace Billing.WebApi.DAL.EFCore
{
    public class BillingContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public BillingContext(DbContextOptions<BillingContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=HwProj_DB;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "Tom", Age = 36 });
        }

    }
}
