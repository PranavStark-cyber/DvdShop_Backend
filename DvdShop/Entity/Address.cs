using System.ComponentModel.DataAnnotations;

namespace DvdShop.Entity
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
       
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }

    }
}
