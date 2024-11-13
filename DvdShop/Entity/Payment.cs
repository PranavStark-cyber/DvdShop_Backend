namespace DvdShop.Entity
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string ReferenceId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Guid CustomerId { get; set; } // Foreign key for Customer
        public Customer Customer { get; set; } // Navigation property
    }


}
