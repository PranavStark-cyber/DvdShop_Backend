namespace DvdShop.DTOs.Requests.Customers
{
    public class UpdateCustomerDTO
    {

        public Guid Id { get; set; }
        public string Nic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public UpdateAddressDTO? Address { get; set; }
    }
}
