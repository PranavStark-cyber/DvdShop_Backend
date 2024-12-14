namespace DvdShop.DTOs.Requests
{
    public class ReviewDTO
    {
        public Guid CustomerId { get; set; }
        public Guid DvdId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // Rating from 1 to 5
    }

}
