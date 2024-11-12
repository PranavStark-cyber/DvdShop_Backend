namespace DvdShop.Entity
{
    public class Director
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<DVD> DVDs { get; set; }
    }

}
