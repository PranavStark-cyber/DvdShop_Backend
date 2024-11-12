using DvdShop.Entity;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Database
{
    public class DvdStoreContext : DbContext
    {
        public DvdStoreContext(DbContextOptions<DvdStoreContext> options) : base(options)
        {
        }

        // DbSet only for the main entities
        public DbSet<DVD> DVDs { get; set; }
        public DbSet<Rental> Rentals { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Relationships and Normalization

            // One-to-One relationship between User and Address
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address) // User has one Address
                .WithOne(a => a.User) // Address has one User
                .HasForeignKey<Address>(a => a.UserId); // Foreign key in Address table

            // One-to-Many relationship between User and Notifications
            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications) // User can have many Notifications
                .WithOne(n => n.Receiver) // Each Notification is received by one User
                .HasForeignKey(n => n.ReceiverId); // Foreign key in Notification table

            // Many-to-Many relationship between User and Role via UserRole
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User) // UserRole has one User
                .WithMany(u => u.UserRoles) // User can have many UserRoles
                .HasForeignKey(ur => ur.UserId); // Foreign key in UserRole table

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role) // UserRole has one Role
                .WithMany(r => r.UserRoles) // Role can have many UserRoles
                .HasForeignKey(ur => ur.RoleId); // Foreign key in UserRole table

            // One-to-Many relationship between Customer and Rentals
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer) // Rental belongs to one Customer
                .WithMany(c => c.Rentals) // Customer can have many Rentals
                .HasForeignKey(r => r.CustomerId); // Foreign key in Rental table

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.DVD) // Rental belongs to one DVD
                .WithMany(d => d.Rentals) // DVD can have many Rentals
                .HasForeignKey(r => r.DvdId); // Foreign key in Rental table

            // One-to-Many relationship between Customer and Payments
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Customer) // Payment belongs to one Customer
                .WithMany(c => c.Payments) // Customer can have many Payments
                .HasForeignKey(p => p.CustomerId); // Foreign key in Payment table

            // One-to-Many relationship between DVD and Reviews
            modelBuilder.Entity<Review>()
                .HasOne(r => r.DVD) // Review belongs to one DVD
                .WithMany(d => d.Reviews) // DVD can have many Reviews
                .HasForeignKey(r => r.DvdId); // Foreign key in Review table

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer) // Review belongs to one Customer
                .WithMany(c => c.Reviews) // Customer can have many Reviews
                .HasForeignKey(r => r.CustomerId); // Foreign key in Review table

            // One-to-One relationship between DVD and Inventory
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.DVD) // Inventory belongs to one DVD
                .WithOne(d => d.Inventory) // DVD has one Inventory
                .HasForeignKey<Inventory>(i => i.DvdId); // Foreign key in Inventory table

            // One-to-Many relationship between DVD and Reservations
            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.DVD) // Reservation belongs to one DVD
                .WithMany(d => d.Reservations) // DVD can have many Reservations
                .HasForeignKey(res => res.DvdId); // Foreign key in Reservation table

            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.Customer) // Reservation belongs to one Customer
                .WithMany(c => c.Reservations) // Customer can have many Reservations
                .HasForeignKey(res => res.CustomerId); // Foreign key in Reservation table

            // One-to-Many relationship between Genre and DVDs
            modelBuilder.Entity<DVD>()
                .HasOne(d => d.Genre) // DVD belongs to one Genre
                .WithMany(g => g.DVDs) // Genre can have many DVDs
                .HasForeignKey(d => d.GenreId); // Foreign key in DVD table

            // One-to-Many relationship between Director and DVDs
            modelBuilder.Entity<DVD>()
                .HasOne(d => d.Director) // DVD belongs to one Director
                .WithMany(dir => dir.DVDs) // Director can have many DVDs
                .HasForeignKey(d => d.DirectorId); // Foreign key in DVD table

            // One-to-One relationship between User and OTP
            modelBuilder.Entity<User>()
                .HasOne(u => u.OTP) // User has one OTP
                .WithOne(o => o.User) // OTP belongs to one User
                .HasForeignKey<OTP>(o => o.UserId); // Foreign key in OTP table

            // Set GUID as the default for primary keys (auto-generation)
            modelBuilder.Entity<DVD>().Property(d => d.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Rental>().Property(r => r.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Payment>().Property(p => p.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Notification>().Property(n => n.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Address>().Property(a => a.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Review>().Property(r => r.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Inventory>().Property(i => i.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Customer>().Property(c => c.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Staff>().Property(s => s.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<User>().Property(u => u.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Role>().Property(r => r.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<UserRole>().Property(ur => ur.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<OTP>().Property(o => o.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Reservation>().Property(r => r.Id).HasDefaultValueSql("NEWID()");

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }

}
