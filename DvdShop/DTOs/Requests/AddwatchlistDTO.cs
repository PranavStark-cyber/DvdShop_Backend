namespace DvdShop.DTOs.Requests
{
    public class AddwatchlistDTO
    {
        public Guid CustomerId { get; set; }
        public Guid DVDId { get; set; }
    }
}
