namespace DvdShop.Entity
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<DVD> DVDs { get; set; }
    }

}
