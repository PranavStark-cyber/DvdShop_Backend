using System.ComponentModel.DataAnnotations;

namespace DvdShop.DTOs.Requests.Manager
{
    public class CreateDvdDto
    {
        public string Title { get; set; }

        public int GenreId { get; set; }

        //[Required(ErrorMessage = "GenreName is required when GenreId is not provided.")]
        public string? GenreName { get; set; }

        public int DirectorId { get; set; }
        public string? DirectorName { get; set; }
        public string? DirectorDescription { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int TotalCopies { get; set; }
    }



}
