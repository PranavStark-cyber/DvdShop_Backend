namespace DvdShop.DTOs.Requests.Rental
{
    public class CreateRentalDto
    {
        public Guid DvdId { get; set; }
        public Guid CustomerId { get; set; }
        public int RentalDays { get; set; }  // Number of days to rent
        public DateTime RequestDate { get; set; }
        public int CopySofDvd { get; set; }

    }
}
