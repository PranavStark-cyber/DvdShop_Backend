using DvdShop.Entity;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Database
{
    public class DvdDbcontext : DbContext
    {
        public DvdDbcontext(DbContextOptions<DvdDbcontext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>()
                 .HasMany(t => t.Address)
                 .WithOne(u => u.Customer)
                 .HasForeignKey(t => t.CustomerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
