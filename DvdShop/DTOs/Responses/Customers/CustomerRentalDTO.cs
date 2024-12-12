using DvdShop.Entity;

namespace DvdShop.DTOs.Responses.Customers
{
    public class CustomerRentalDTO
    {
        public Guid Id { get; set; }
        public string Nic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }

    }
}
