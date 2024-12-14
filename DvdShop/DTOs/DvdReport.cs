namespace DvdShop.DTOs
{
    public class DvdReport
    {
        public Guid DvdId { get; set; }
        public string Title { get; set; }
        public int TotalInventory { get; set; }
        public int RentedOut { get; set; }
    }
}
