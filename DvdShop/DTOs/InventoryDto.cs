namespace DvdShop.DTOs
{
    public class InventoryDto
    {
        public Guid DvdId { get; set; }
        public string DvdTitle { get; set; }  // Optional: For DVD title
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }

}
