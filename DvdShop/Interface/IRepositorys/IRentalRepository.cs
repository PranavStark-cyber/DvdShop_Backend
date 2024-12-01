using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllRentals();
        Task<Rental> GetRentalById(Guid id);
        Task CreateRental(Rental rental);
        Task UpdateRental(Rental rental);
        Task DeleteRental(Guid id);
        Task<IEnumerable<Rental>> GetRentalsByCustomerId(Guid customerId);
    }
}
