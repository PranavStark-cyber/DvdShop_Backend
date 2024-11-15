using DvdShop.DTOs.Requests.Manager;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;

namespace DvdShop.Services
{
    public class ManagerService:IManagerService
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }


        public async Task<DVD> AddDvdAsync(CreateDvdDto createDvdDto)
        {
            var genre = await _managerRepository.GetOrCreateGenreAsync(createDvdDto.GenreId, createDvdDto.GenreName);

            var director = await _managerRepository.GetOrCreateDirectorAsync(createDvdDto.DirectorId, createDvdDto.DirectorName, createDvdDto.DirectorDescription);

            var dvd = new DVD
            {
                Id = Guid.NewGuid(),
                Title = createDvdDto.Title,
                GenreId = genre.Id,  
                DirectorId = director.Id,  
                ReleaseDate = createDvdDto.ReleaseDate,
                Price = createDvdDto.Price,
                Description = createDvdDto.Description,
                ImageUrl = createDvdDto.ImageUrl ?? "default-image-url.jpg",
                Rentals = new List<Rental>(),
                Reviews = new List<Review>(),
                Reservations = new List<Reservation>()
            };

            var addedDvd = await _managerRepository.AddDvdAsync(dvd);

            var inventory = new Inventory
            {
                Id = Guid.NewGuid(),
                DvdId = addedDvd.Id,
                TotalCopies = createDvdDto.TotalCopies,
                AvailableCopies = createDvdDto.TotalCopies,
                LastRestock = DateTime.UtcNow,
                Dvd = addedDvd
            };

            await _managerRepository.AddInventoryAsync(inventory);

            return addedDvd;
        }




        public async Task<DVD> GetDvdByIdAsync(Guid id)
        {
            return await _managerRepository.GetDvdByIdAsync(id);
        }
    }
}
