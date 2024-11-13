namespace DvdShop.Entity
{
    public class OTP
    {
        public Guid Id { get; set; } 
        public Guid UserId { get; set; }  
        public string Type { get; set; }  // "EmailVerification" or "PasswordReset"
        public string Code { get; set; }  
        public DateTime ExpiryDate { get; set; }

        public User User { get; set; }  
    }

}
