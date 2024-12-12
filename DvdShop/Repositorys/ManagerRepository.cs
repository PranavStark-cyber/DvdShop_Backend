using DvdShop.Database;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class ManagerRepository:IManagerRepository
    {
        private readonly DvdStoreContext _storeContext;

        public ManagerRepository(DvdStoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<Staff> GetStaffById(Guid StaffId)
        {
            try
            {
                var staff = await _storeContext.Staffs
              .FirstOrDefaultAsync(c => c.Id == StaffId);


                if (staff == null)
                {
                    return null;
                }
                return staff;

            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching customer from database.", ex);
            }



        }
       

        public async Task<DVD> AddDvd(DVD dvd)
        {
            _storeContext.DVDs.Add(dvd);
            await _storeContext.SaveChangesAsync();
            return dvd;
        }

        public async Task<Genre> GetGenreById(int genreId)
        {
            return await _storeContext.DVDs.Where(d => d.GenreId == genreId).Select(d => d.Genre).FirstOrDefaultAsync();
        }
        public async Task<Director> GetDirectorById(int directorId)
        {
            return await _storeContext.DVDs.Where(d => d.DirectorId == directorId).Select(d => d.Director).FirstOrDefaultAsync();
        }

        //public async Task<Genre> GetOrCreateGenreAsync(int genreId, string genreName)
        //{
        //    var genre = await _storeContext.Genres.Where(g => g.Id == genreId).FirstOrDefaultAsync();


        //    if (genre == null)
        //    {
        //        genre = new Genre
        //        {
        //            Name = genreName
        //        };

        //        _storeContext.Genres.Add(genre);
        //        await _storeContext.SaveChangesAsync();
        //    }


        //    return genre;
        //}
        public async Task<Genre> GetOrCreateGenre(int genreId, string genreName)
        {
            Genre genre = null;

            if (genreId != null)
            {
                genre = await _storeContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
            }

            if (genre == null)
            {
                genre = await _storeContext.Genres.FirstOrDefaultAsync(g => g.Name == genreName);

                if (genre == null)
                {
                    genre = new Genre
                    {
                        Name = genreName
                    };

                    _storeContext.Genres.Add(genre);
                    await _storeContext.SaveChangesAsync();
                }
            }

            return genre;
        }


        public async Task<Director> GetOrCreateDirector(int directorId, string directorName, string directorDescription)
        {
            var director = await _storeContext.Directors.FirstOrDefaultAsync(d => d.Id == directorId);

            if (director == null)
            {
                director = await _storeContext.Directors.FirstOrDefaultAsync(d => d.Name == directorName);

                if (director == null)
                {
                    director = new Director
                    {
                        Name = directorName,
                        Decriptions = directorDescription
                    };

                    _storeContext.Directors.Add(director);
                    await _storeContext.SaveChangesAsync();
                }
            }

            return director;
        }

        // Get Inventory by DVD ID
        public async Task<Inventory> GetInventoryByDvdId(Guid dvdId)
        {
            return await _storeContext.Inventories.FirstOrDefaultAsync(i => i.DvdId == dvdId);
        }


        public async Task<Inventory> AddInventory(Inventory inventory)
        {
            _storeContext.Inventories.Add(inventory);
            await _storeContext.SaveChangesAsync();
            return inventory;
        }

        public async Task<DVD> GetDvdById(Guid id)
        {
            return await _storeContext.DVDs.Include(d => d.Genre).Include(d => d.Director).Include(d=>d.Inventory).FirstOrDefaultAsync(d => d.Id == id);
        }
        //public async Task<DVD> GetDvdById(Guid id)
        //{
        //    // Perform JOIN on the Inventory table
        //    var dvd = await (from d in _storeContext.DVDs
        //                     join inv in _storeContext.Inventory on d.Id equals inv.DvdId into inventoryJoin
        //                     from inv in inventoryJoin.DefaultIfEmpty() // To handle cases where Inventory is null (left join)
        //                     where d.Id == id
        //                     select new DVD
        //                     {
        //                         Id = d.Id,
        //                         Title = d.Title,
        //                         Genre = d.Genre,
        //                         Director = d.Director,
        //                         Inventory = inv // Join the inventory, can be null
        //                     }).FirstOrDefaultAsync();

        //    return dvd;
        //}

        // Update DVD
        public async Task<DVD> UpdateDvd(DVD dvd)
        {
            _storeContext.DVDs.Update(dvd);
            await _storeContext.SaveChangesAsync();
            return dvd;
        }

        public async Task<string> DeleteDvd(Guid id, int quantityToDelete)
        {
            var dvd = await _storeContext.DVDs.FirstOrDefaultAsync(d => d.Id == id);
            if (dvd == null)
            {
                throw new KeyNotFoundException("DVD not found.");
            }

            var inventory = await _storeContext.Inventories.FirstOrDefaultAsync(i => i.DvdId == id);
            if (inventory == null)
            {
                throw new KeyNotFoundException("Inventory for the specified DVD not found.");
            }

            if (inventory.AvailableCopies < quantityToDelete)
            {
                throw new InvalidOperationException("Not enough copies available to delete.");
            }
            if (inventory.TotalCopies < quantityToDelete)
            {
                throw new InvalidOperationException("Not enough copies available to delete.");
            }

            inventory.AvailableCopies -= quantityToDelete;
            inventory.TotalCopies -= quantityToDelete;

            // Check if the DVD has no remaining copies
            if (inventory.AvailableCopies == 0 || inventory.TotalCopies == 0)
            {
                _storeContext.Inventories.Remove(inventory); // Remove the inventory record

               
                _storeContext.DVDs.Remove(dvd);
            }
            else
            {
                _storeContext.Inventories.Update(inventory); 
            }

            await _storeContext.SaveChangesAsync();

            return $"Successfully deleted {quantityToDelete} copies of '{dvd.Title}'.";
        }



        // Get All DVDs
        public async Task<IEnumerable<DVD>> GetAllDvds()
        {
            return await _storeContext.DVDs.Include(d => d.Genre).Include(d => d.Director).Include(d=>d.Inventory).ToListAsync();
        }

        public async Task<List<Genre>> GetGenre()
        {
            return await _storeContext.Genres.ToListAsync();
        }

        public async Task<List<Director>> GetDirector()
        {
            return await _storeContext.Directors.ToListAsync();
        }

        // Update Inventory
        public async Task UpdateInventory(Inventory inventory)
        {
            _storeContext.Inventories.Update(inventory);
            await _storeContext.SaveChangesAsync();
        }

     


        // Remove Inventory
        public async Task RemoveInventory(Inventory inventory)
        {
            _storeContext.Inventories.Remove(inventory);
            await _storeContext.SaveChangesAsync();
        }

        public async Task<List<Inventory>> GetAllInventory()
        {
            try
            {
                return await _storeContext.Inventories
                                     .Include(i => i.Dvd)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching inventory from the database.", ex);
            }
        }

        public async Task<List<Inventory>> GetWeeklyInventoryReport()
        {
            try
            {
                var oneWeekAgo = DateTime.Now.AddDays(-7);
                return await _storeContext.Inventories
                                     .Include(i => i.Dvd)
                                     .Where(i => i.LastRestock >= oneWeekAgo)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching weekly inventory report.", ex);
            }
        }

        public async Task<List<Inventory>> GetMonthlyInventoryReport()
        {
            try
            {
                var oneMonthAgo = DateTime.Now.AddMonths(-1);
                return await _storeContext.Inventories
                                     .Include(i => i.Dvd)
                                     .Where(i => i.LastRestock >= oneMonthAgo)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching monthly inventory report.", ex);
            }
        }

        public async Task SendInventoryReportNotification(string title, string message)
        {
            try
            {
                // Fetch managers
                var managers = await _storeContext.Users
                               .Where(u => _storeContext.UserRoles
                               .Where(ur => ur.UserId == u.Id)
                               .Join(_storeContext.Roles,
                                ur => ur.RoleId,
                                r => r.Id,
                                (ur, r) => r.Name)
                               .Contains("Manager"))
                               .ToListAsync();

                if (!managers.Any())
                {
                    throw new Exception("No managers found to send notifications.");
                }

                // Create notifications for managers
                var notifications = managers.Select(manager => new Notification
                {
                    Id = Guid.NewGuid(),
                    ReceiverId = manager.Id,
                    Title = title,
                    Message = message,
                    ViewStatus = "Unread",
                    Type = "Info",
                    Date = DateTime.Now
                }).ToList();

                await _storeContext.Notifications.AddRangeAsync(notifications);
                await _storeContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending inventory report notification.", ex);
            }
        }
    }
}
