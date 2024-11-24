using DvdShop.Entity;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Database
{
    public class DvdStoreContext : DbContext
    {
        public DvdStoreContext(DbContextOptions<DvdStoreContext> options) : base(options)
        {
        }


        public DbSet<DVD> DVDs { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Many-to-Many relationship between User and Role via UserRole
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // One-to-Many relationship between Customer and Rentals
            modelBuilder.Entity<Rental>()
    .HasOne(r => r.DVD)
    .WithMany(d => d.Rentals)
    .HasForeignKey(r => r.DvdId)
    .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete for DVD

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many relationship between Customer and Payments
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CustomerId);

            // One-to-Many relationship between DVD and Reviews
            modelBuilder.Entity<Review>()
                .HasOne(r => r.DVD)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DvdId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId);

            // One-to-One relationship between DVD and Inventory
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Dvd)
                .WithOne(d => d.Inventory)
                .HasForeignKey<Inventory>(i => i.DvdId);

            // One-to-Many relationship between DVD and Reservations
            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.DVD)
                .WithMany(d => d.Reservations)
                .HasForeignKey(res => res.DvdId);

            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(res => res.CustomerId);

            // One-to-Many relationship between Genre and DVDs
            modelBuilder.Entity<DVD>()
                .HasOne(d => d.Genre)
                .WithMany(g => g.DVDs)
                .HasForeignKey(d => d.GenreId);

            // One-to-Many relationship between Director and DVDs
            modelBuilder.Entity<DVD>()
                .HasOne(d => d.Director)
                .WithMany(dir => dir.DVDs)
                .HasForeignKey(d => d.DirectorId);

            // One-to-Many relationship between Staff and Customers (optional, if required)
            // modelBuilder.Entity<Staff>()
            //     .HasMany(s => s.Customers)
            //     .WithOne(c => c.Staff)
            //     .HasForeignKey(c => c.StaffId);  // Uncomment if a Staff is associated with Customers


            // Optional: Enum handling for RentalStatus in Rental model
            modelBuilder.Entity<Rental>()
                .Property(r => r.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (RentalStatus)Enum.Parse(typeof(RentalStatus), v));

            // Optional: Length constraints on string fields
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.PhoneNumber)
                .HasMaxLength(15); // Set phone number length

            modelBuilder.Entity<Staff>()
                .Property(s => s.NIC)
                .HasMaxLength(20); // Set NIC length

            base.OnModelCreating(modelBuilder);
        }
    }
}
