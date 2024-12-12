using DvdShop.Database;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;
using System;

namespace DvdShop.Repositorys
{
    public class RentalRepository:IRentalRepository
    {
        private readonly DvdStoreContext _rentalcontext;

        public RentalRepository(DvdStoreContext rentalcontext)
        {
            _rentalcontext = rentalcontext;
        }

        public async Task<IEnumerable<Rental>> GetAllRentals()
        {
            return await _rentalcontext.Rentals.Include(r => r.DVD).Include(r => r.Customer).ToListAsync();
        }

        public async Task<Rental> GetRentalById(Guid id)
        {
            return await _rentalcontext.Rentals.Include(r => r.DVD).Include(r => r.Customer).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task CreateRental(Rental rental)
        {
            _rentalcontext.Rentals.Add(rental);
            await _rentalcontext.SaveChangesAsync();
        }

        public async Task UpdateRental(Rental rental)
        {
            _rentalcontext.Rentals.Update(rental);
            await _rentalcontext.SaveChangesAsync();
        }

        public async Task DeleteRental(Guid id)
        {
            var rental = await _rentalcontext.Rentals.FindAsync(id);
            if (rental != null)
            {
                _rentalcontext.Rentals.Remove(rental);
                await _rentalcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Rental>> GetRentalsByCustomerId(Guid customerId)
        {
            return await _rentalcontext.Rentals
                                 .Where(r => r.CustomerId == customerId)
                                 .Include(r => r.DVD).Include(r=>r.Customer)
                                 .ToListAsync();
        }

    }
}
