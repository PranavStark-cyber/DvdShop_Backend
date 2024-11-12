using static System.Net.WebRequestMethods;

namespace DvdShop.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsConfirmed { get; set; }

        public Address Address { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public OTP OTP { get; set; }
    }

}
