using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;

namespace DvdShop.DTOs.Responses.Customers
{
    public class CustomerResponseDTO
    {

        public Guid Id { get; set; }
        public string Nic { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }

        public ICollection<Rental>? Rentals { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public AddressResponse? Address { get; set; }
    }
}
