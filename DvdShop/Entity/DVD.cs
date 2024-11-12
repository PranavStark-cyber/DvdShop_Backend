using System.IO;

namespace DvdShop.Entity
{
    public class DVD
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid GenreId { get; set; }
        public Guid DirectorId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Genre Genre { get; set; }
        public Director Director { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Inventory Inventory { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }

}
