namespace DvdShop.Entity
{
    public class Staff
    {
        public Guid Id { get; set; }
        public string NIC { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; } // Enum: Manager, Employee
    }

    public enum Type
    {
        Manager =1,
        Employee =2
    }

}
