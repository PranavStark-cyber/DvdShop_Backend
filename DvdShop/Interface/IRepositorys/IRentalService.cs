using DvdShop.DTOs.Requests.Rental;
using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface IRentalService
    {
        //Task<List<Rental>> GetAllRentalsAsync();
        //Task<Rental?> GetRentalByIdAsync(Guid rentalId);
        //Task<Rental> CreateRentalAsync(CreateRentalDto createRentalDto);
        //Task<Rental?> ApproveRentalAsync(Guid rentalId);
        //Task<Rental?> RejectRentalAsync(Guid rentalId);
        //Task<Rental?> CollectRentalAsync(Guid rentalId);
        //Task<Rental?> ReturnRentalAsync(Guid rentalId);
        //Task<List<Rental>> GetRentalsByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<Rental>> GetAllRentals();
        Task<Rental> GetRentalById(Guid id);
        Task ApproveRental(Guid id);
        Task CollectRental(Guid id);
        Task RequestRental(Rental rental);
        Task ReturnRental(Guid id);
        Task RejectRental(Guid id);
        Task<List<Rental>> GetRentalsByCustomerId(Guid customerId);
    }
}
