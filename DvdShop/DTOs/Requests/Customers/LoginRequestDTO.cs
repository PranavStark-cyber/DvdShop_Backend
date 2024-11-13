using System.ComponentModel.DataAnnotations;

namespace DvdShop.DTOs.Requests.Customers
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; }= string.Empty;
    }
}
