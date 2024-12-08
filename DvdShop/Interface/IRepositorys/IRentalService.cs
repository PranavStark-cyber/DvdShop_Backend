using DvdShop.DTOs.Requests.Rental;
using DvdShop.DTOs.Responses.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface IRentalService
    {

        Task<IEnumerable<Rental>> GetAllRentals();
        Task<Rental> GetRentalById(Guid id);
        Task ApproveRental(Guid id);
        Task CollectRental(Guid id);
        Task RequestRental(CreateRentalDto createRentalDto);
        Task ReturnRental(Guid id);
        Task RejectRental(Guid id);
        Task<List<RentalResponse>> GetRentalsByCustomerId(Guid customerId);
    }
}
