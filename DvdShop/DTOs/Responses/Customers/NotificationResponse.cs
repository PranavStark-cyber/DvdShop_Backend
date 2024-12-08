namespace DvdShop.DTOs.Responses.Customers
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ViewStatus { get; set; } // Read/Unread
        public string Type { get; set; } // Info/Warning/Error
        public DateTime Date { get; set; }
    }
}
