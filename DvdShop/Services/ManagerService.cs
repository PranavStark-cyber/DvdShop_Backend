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

        // Update DVD
        public async Task<DVD> UpdateDvdAsync(Guid id, CreateDvdDto updateDvdDto)
        {
            var dvd = await _managerRepository.GetDvdByIdAsync(id);
            if (dvd == null)
            {
                throw new KeyNotFoundException("DVD not found.");
            }

            // Update DVD details
            dvd.Title = updateDvdDto.Title ?? dvd.Title;
            dvd.Description = updateDvdDto.Description ?? dvd.Description;
            if (updateDvdDto.Price != 0) 
            {
                dvd.Price = updateDvdDto.Price;
            }

            if (updateDvdDto.ReleaseDate != default(DateTime))
            {
                dvd.ReleaseDate = updateDvdDto.ReleaseDate;
            }

            dvd.ImageUrl = updateDvdDto.ImageUrl ?? dvd.ImageUrl;

            var genre = await _managerRepository.GetOrCreateGenreAsync(updateDvdDto.GenreId, updateDvdDto.GenreName);
            dvd.GenreId = genre.Id;

            var director = await _managerRepository.GetOrCreateDirectorAsync(updateDvdDto.DirectorId, updateDvdDto.DirectorName, updateDvdDto.DirectorDescription);
            dvd.DirectorId = director.Id;

            var updatedDvd = await _managerRepository.UpdateDvdAsync(dvd);

            var inventory = await _managerRepository.GetInventoryByDvdIdAsync(dvd.Id);
            if (inventory != null)
            {
                inventory.TotalCopies = updateDvdDto.TotalCopies;
                inventory.AvailableCopies = updateDvdDto.TotalCopies;
                await _managerRepository.UpdateInventoryAsync(inventory);
            }

            return updatedDvd;
        }

        public async Task<string> DeleteDvdAsync(Guid id, int quantityToDelete)
        {
            try
            {
                if (quantityToDelete <= 0)
                {
                    throw new ArgumentException("Quantity to delete must be greater than zero.", nameof(quantityToDelete));
                }

                string result = await _managerRepository.DeleteDvdAsync(id, quantityToDelete);

                return result;
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException("Invalid quantity provided.", ex);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException("The specified DVD or inventory does not exist.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Error processing the deletion request. Not enough copies available.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while attempting to delete DVD copies.", ex);
            }
        }


        public async Task<IEnumerable<DVD>> GetAllDvdsAsync()
        {
            return await _managerRepository.GetAllDvdsAsync();
        }

    }

}

