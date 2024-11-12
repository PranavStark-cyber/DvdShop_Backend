namespace DvdShop.Entity
{
    public class OTP
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; }

        public User User { get; set; }
    }

}
