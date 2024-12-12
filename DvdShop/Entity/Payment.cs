namespace DvdShop.Entity
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid ReferenceId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Customer Reference { get; set; } // Navigation property
    }


}
