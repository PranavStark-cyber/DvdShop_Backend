using DvdShop.DTOs.Requests.Manager;
using DvdShop.DTOs.Responses.Customers;
using DvdShop.DTOs.Responses.Customers.Manager;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;
using DvdShop.Repositorys;

namespace DvdShop.Services
{
    public class ManagerService:IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly ICustomerRepository _customerRepository;

        public ManagerService(IManagerRepository managerRepository, ICustomerRepository customerRepository)
        {
            _managerRepository = managerRepository;
            _customerRepository = customerRepository;
        }


        public async Task<DVD> AddDvd(CreateDvdDto createDvdDto)
        {
            var genre = await _managerRepository.GetOrCreateGenre(createDvdDto.GenreId, createDvdDto.GenreName);

            var director = await _managerRepository.GetOrCreateDirector(createDvdDto.DirectorId, createDvdDto.DirectorName, createDvdDto.DirectorDescription);

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

            var addedDvd = await _managerRepository.AddDvd(dvd);

            var inventory = new Inventory
            {
                Id = Guid.NewGuid(),
                DvdId = addedDvd.Id,
                TotalCopies = createDvdDto.TotalCopies,
                AvailableCopies = createDvdDto.TotalCopies,
                LastRestock = DateTime.UtcNow,
                Dvd = addedDvd
            };

            await _managerRepository.AddInventory(inventory);

            return addedDvd;
        }




        public async Task<DVD> GetDvdById(Guid id)
        {
            return await _managerRepository.GetDvdById(id);
        }

        // Update DVD
        public async Task<DVD> UpdateDvd(Guid id, CreateDvdDto updateDvdDto)
        {
            var dvd = await _managerRepository.GetDvdById(id);
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
            dvd.BackgroundImageurl = updateDvdDto.BackgroundImageurl ?? dvd.BackgroundImageurl;
            dvd.Trailers = updateDvdDto.Trailers ?? dvd.Trailers;

            var genre = await _managerRepository.GetOrCreateGenre(updateDvdDto.GenreId, updateDvdDto.GenreName);
            dvd.GenreId = genre.Id;

            var director = await _managerRepository.GetOrCreateDirector(updateDvdDto.DirectorId, updateDvdDto.DirectorName, updateDvdDto.DirectorDescription);
            dvd.DirectorId = director.Id;

            var updatedDvd = await _managerRepository.UpdateDvd(dvd);

            // Check if inventory exists
            var inventory = await _managerRepository.GetInventoryByDvdId(dvd.Id);
            if (inventory != null)
            {
                // Update existing inventory
                inventory.TotalCopies = updateDvdDto.TotalCopies;
                inventory.AvailableCopies = updateDvdDto.TotalCopies;
                await _managerRepository.UpdateInventory(inventory);
            }
            else
            {
                // Create new inventory if it doesn't exist
                var newInventory = new Inventory
                {
                    DvdId = dvd.Id,
                    TotalCopies = updateDvdDto.TotalCopies,
                    AvailableCopies = updateDvdDto.TotalCopies,
                    LastRestock = DateTime.UtcNow // Set the current date and time for the last restock
                };

                // Add the new inventory to the database
                await _managerRepository.AddInventory(newInventory);
            }

            return updatedDvd;
        }

        public async Task<string> DeleteDvd(Guid id, int quantityToDelete)
        {
            try
            {
                if (quantityToDelete <= 0)
                {
                    throw new ArgumentException("Quantity to delete must be greater than zero.", nameof(quantityToDelete));
                }

                string result = await _managerRepository.DeleteDvd(id, quantityToDelete);

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


        public async Task<IEnumerable<DVD>> GetAllDvds()
        {
            return await _managerRepository.GetAllDvds();
        }

        public async Task<List<Genre>> GetGenare()
        {
            return await _managerRepository.GetGenre();
        }
        public async Task<List<Director>> GetDirector()
        {
            return await _managerRepository.GetDirector();
        }

        public async Task<List<Inventory>> GetAllInventory()
        {
            try
            {
                return await _managerRepository.GetAllInventory();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all inventory.", ex);
            }
        }

        public async Task<List<Inventory>> GetWeeklyInventoryReport()
        {
            try
            {
                return await _managerRepository.GetWeeklyInventoryReport();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the weekly inventory report.", ex);
            }
        }

        public async Task<List<Inventory>> GetMonthlyInventoryReport()
        {
            try
            {
                return await _managerRepository.GetMonthlyInventoryReport();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the monthly inventory report.", ex);
            }
        }

        public async Task SendInventoryReportNotification(string type)
        {
            try
            {
                List<Inventory> report;

                if (type == "Weekly")
                {
                    report = await GetWeeklyInventoryReport();
                }
                else if (type == "Monthly")
                {
                    report = await GetMonthlyInventoryReport();
                }
                else
                {
                    throw new ArgumentException("Invalid report type. Use 'Weekly' or 'Monthly'.");
                }

                var message = $"Inventory Report ({type}):\n\n" +
                              string.Join("\n", report.Select(i =>
                                  $"- {i.Dvd.Title}: {i.AvailableCopies}/{i.TotalCopies} available"));

                await _managerRepository.SendInventoryReportNotification($"Inventory {type} Report", message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while sending the inventory report notification.", ex);
            }
        }

        public async Task<ManagerResponseDTO?> GetStaffById(Guid managerId)
        {
            try
            {
                var manager = await _managerRepository.GetStaffById(managerId);
                if (manager == null)
                {
                    throw new KeyNotFoundException($"staff with ID {managerId} was not found.");
                }

                var user = await _customerRepository.GetUserById(managerId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {managerId} was not found.");
                }

                var managerdata = new ManagerResponseDTO
                {
                    Id = managerId,
                    Email = user.Email,
                    NIC = manager.NIC,
                    FirstName = manager.FirstName,
                    LastName = manager.LastName,

                };
                return managerdata;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the staff.", ex);
            }
        }


    }

}

