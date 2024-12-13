namespace DvdShop.Entity
{
    public class AddWatchlist
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid DVDId { get; set; }
        public Customer Customer { get; set; }
        public DVD DVD { get; set; }
    }
}
