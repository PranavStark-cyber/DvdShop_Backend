namespace DvdShop.DTOs.Responses.Customers
{
    public class TotalAmountResponse
    {
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
