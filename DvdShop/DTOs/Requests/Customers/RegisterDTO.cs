using DvdShop.Entity;

namespace DvdShop.DTOs.Requests.Customers
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public int? Mobilenumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nic { get; set; }
        public string Password { get; set; }
        public string? Review { get; set; }
        public ICollection<AddressRequest>? Address { get; set; }
    }
}
