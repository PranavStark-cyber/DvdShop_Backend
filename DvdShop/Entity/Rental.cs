namespace DvdShop.Entity
{
    public class Rental
    {
        public Guid Id { get; set; }
        public Guid DvdId { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; } // Enum for status
        public string DaysofRent {  get; set; } 
        public DateTime RequestDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? CollectedDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DVD DVD { get; set; }
        public Customer Customer { get; set; }
    }

}
