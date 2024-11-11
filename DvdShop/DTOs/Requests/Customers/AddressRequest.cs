using DvdShop.Entity;

namespace DvdShop.DTOs.Requests.Customers
{
    public class AddressRequest
    {

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
    }
}
