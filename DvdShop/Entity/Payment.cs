namespace DvdShop.Entity
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid ReferenceId { get; set; } // Foreign key for Rental or other reference
        public string Type { get; set; } // Enum for Payment Type (e.g., Credit, Debit)
        public decimal Amount { get; set; }

        public Customer Customer { get; set; } // Navigation property for Customer (Many-to-One)
    }

}
