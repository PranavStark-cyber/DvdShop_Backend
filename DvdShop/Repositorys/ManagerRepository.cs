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


    }
}
