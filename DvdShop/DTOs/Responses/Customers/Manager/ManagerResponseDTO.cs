namespace DvdShop.DTOs.Responses.Customers.Manager
{
    public class ManagerResponseDTO
    {
        public Guid Id { get; set; }
        public string NIC { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
