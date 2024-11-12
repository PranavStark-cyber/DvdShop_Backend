namespace DvdShop.Entity
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid DvdId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int ReservationDays { get; set; }
        public DateTime ExpiryDate { get; set; }

        public DVD DVD { get; set; }
        public Customer Customer { get; set; }
    }

}
