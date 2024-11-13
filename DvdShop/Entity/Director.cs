namespace DvdShop.Entity
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Decriptions { get; set; }

        public ICollection<DVD> DVDs { get; set; }
    }

}
