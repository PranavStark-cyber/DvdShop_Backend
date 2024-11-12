namespace DvdShop.Entity
{
    public class Staff
    {
        public Guid Id { get; set; }
        public string NIC { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Enum: Manager, Employee
    }

}
