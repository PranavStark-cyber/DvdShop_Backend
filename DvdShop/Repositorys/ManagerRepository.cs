using DvdShop.Database;
using DvdShop.Entity;

namespace DvdShop.Repositorys
{
    public class ManagerRepository
    {
        private readonly DvdStoreContext _storeContext;

        public ManagerRepository(DvdStoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<DVD> AddDvd(DVD dvd)
        {
            await _storeContext.AddAsync(dvd);
            await _storeContext.SaveChangesAsync(); 
            return dvd;
        }

        
    }
}
