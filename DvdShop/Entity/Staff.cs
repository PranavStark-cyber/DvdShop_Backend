namespace DvdShop.Entity
{
    public class Staff
    {
        public Guid Id { get; set; }
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Type { get; set; } // Enum: Manager, Employee
    }

    //public enum Type
    //{
    //    Manager =1,
    //    Employee =2
    //}

}
