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
            var staff = await _storeContext.Staffs
              .FirstOrDefaultAsync(c => c.Id == StaffId);


            if (staff == null)
            {
                return null;
            }



            return staff;
        }

        public async Task<DVD> AddDvdAsync(DVD dvd)
        {
            _storeContext.DVDs.Add(dvd);
            await _storeContext.SaveChangesAsync();
            return dvd;
        }

        public async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            return await _storeContext.DVDs.Where(d => d.GenreId == genreId).Select(d => d.Genre).FirstOrDefaultAsync();
        }
        public async Task<Director> GetDirectorByIdAsync(int directorId)
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
        public async Task<Genre> GetOrCreateGenreAsync(int genreId, string genreName)
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


        public async Task<Director> GetOrCreateDirectorAsync(int directorId, string directorName, string directorDescription)
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
        public async Task<Inventory> GetInventoryByDvdIdAsync(Guid dvdId)
        {
            return await _storeContext.Inventories.FirstOrDefaultAsync(i => i.DvdId == dvdId);
        }


        public async Task<Inventory> AddInventoryAsync(Inventory inventory)
        {
            _storeContext.Inventories.Add(inventory);
            await _storeContext.SaveChangesAsync();
            return inventory;
        }

        public async Task<DVD> GetDvdByIdAsync(Guid id)
        {
            return await _storeContext.DVDs.Include(d => d.Genre).Include(d => d.Director).FirstOrDefaultAsync(d => d.Id == id);
        }

        // Update DVD
        public async Task<DVD> UpdateDvdAsync(DVD dvd)
        {
            _storeContext.DVDs.Update(dvd);
            await _storeContext.SaveChangesAsync();
            return dvd;
        }

        public async Task<string> DeleteDvdAsync(Guid id, int quantityToDelete)
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
        public async Task<IEnumerable<DVD>> GetAllDvdsAsync()
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
        public async Task UpdateInventoryAsync(Inventory inventory)
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


    }
}
