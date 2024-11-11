using System.ComponentModel.DataAnnotations;

namespace DvdShop.Entity
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public int? Mobilenumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nic { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Review { get; set; }
        public string Password {  get; set; }
        public ICollection<Address>? Address { get; set; }

    }
}
