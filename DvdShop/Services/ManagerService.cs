﻿using DvdShop.DTOs.Requests.Manager;
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

            var genre = await _managerRepository.GetOrCreateGenre(updateDvdDto.GenreId, updateDvdDto.GenreName);
            dvd.GenreId = genre.Id;

            var director = await _managerRepository.GetOrCreateDirector(updateDvdDto.DirectorId, updateDvdDto.DirectorName, updateDvdDto.DirectorDescription);
            dvd.DirectorId = director.Id;

            var updatedDvd = await _managerRepository.UpdateDvd(dvd);

            var inventory = await _managerRepository.GetInventoryByDvdId(dvd.Id);
            if (inventory != null)
            {
                inventory.TotalCopies = updateDvdDto.TotalCopies;
                inventory.AvailableCopies = updateDvdDto.TotalCopies;
                await _managerRepository.UpdateInventory(inventory);
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
    }

}

