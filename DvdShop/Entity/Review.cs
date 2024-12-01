namespace DvdShop.Entity
{
    public class Review
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; } // Foreign key for Customer
        public Guid DvdId { get; set; } // Foreign key for DVD
        public DateTime ReviewDate { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // Rating from 1 to 5

        public Customer Customer { get; set; } // Navigation property for Customer (Many-to-One)
        public DVD DVD { get; set; } // Navigation property for DVD (Many-to-One)
    }

}
