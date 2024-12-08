namespace DvdShop.DTOs.Responses.Customers
{
    public class OverdueAmountResponse
    {
        public Guid CustomerId { get; set; }
        public decimal OverdueAmount { get; set; }
    }
}
