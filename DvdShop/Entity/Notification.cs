namespace DvdShop.Entity
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid ReceiverId { get; set; } // Foreign key for User
        public string Title { get; set; }
        public string Message { get; set; }
        public string ViewStatus { get; set; } // Enum (e.g., Read, Unread)
        public string Type { get; set; } // Enum (e.g., Info, Warning, Error)
        public DateTime Date { get; set; }

        public User Receiver { get; set; } // Navigation property for Receiver (Many-to-One)
    }

}
