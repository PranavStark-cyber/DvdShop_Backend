namespace DvdShop.Entity
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid DvdId { get; set; } // Foreign key for DVD
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime LastRestock { get; set; }
        public DVD Dvd { get; set; } // Navigation property for DVD (One-to-One)
    }

}
