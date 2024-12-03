namespace DvdShop.Entity
{
    public class Rental
    {
        public Guid Id { get; set; }
        public Guid DvdId { get; set; }
        public Guid CustomerId { get; set; }
        public int RentalDays { get; set; }
        public RentalStatus Status { get; set; } // Use the enum here
        public DateTime RequestDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? CollectedDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DVD DVD { get; set; }
        public Customer Customer { get; set; }
    }

    public enum RentalStatus
    {
        Request,
        Approved,
        Collected,
        Returned,
        Rejected
    }

}
