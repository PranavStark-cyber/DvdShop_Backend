using DvdShop.Entity;

namespace DvdShop.DTOs.Responses.Customers
{
    public class RentalResponse
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
        public DvdResponse DVD { get; set; }
        public CustomerRentalDTO Customer { get; set; }
    }
}
