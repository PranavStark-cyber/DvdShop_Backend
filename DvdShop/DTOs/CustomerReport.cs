namespace DvdShop.DTOs
{
    public class CustomerReport
    {
        public Guid CustomerId { get; set; }
        public string FullName { get; set; }
        public int TotalRentals { get; set; }
        public decimal TotalAmountSpent { get; set; }
    }
}
