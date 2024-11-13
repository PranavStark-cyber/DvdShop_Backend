using System.ComponentModel.DataAnnotations;

namespace DvdShop.Entity
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public string Nic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }

        public ICollection<Rental>? Rentals { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public Address? Address { get; set; }
    }

}
