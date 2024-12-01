namespace DvdShop.DTOs.Requests.Customers
{
    public class AddReviewDTO
    {
        public Guid CustomerId { get; set; }
        public Guid DvdId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // Must be between 1 and 5
    }
}
