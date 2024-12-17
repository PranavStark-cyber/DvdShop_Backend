using DvdShop.DTOs.Responses.Customers;
using DvdShop.Entity;

namespace DvdShop.DTOs
{
    public class RentalViewModel
    {

        public Guid RentalId { get; set; }
        public int RentalDays { get; set; }
        public int Copies { get; set; }
        public decimal RentalPrice { get; set; } // The calculated rental price (Price * Copies * RentalDays)
        public RentalStatus Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? CollectedDate { get; set; }
        public DvdResponse DVD { get; set; }

        public CustomerRentalDTO Customer { get; set; }
    }

  
}