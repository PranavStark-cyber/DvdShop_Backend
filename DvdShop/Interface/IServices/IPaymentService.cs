using DvdShop.DTOs.Responses.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPayment(Rental rental);
        Task<TotalAmountResponse> GetTotalAmountByCustomerId(Guid customerId);
        Task<OverdueAmountResponse> GetOverdueAmountByCustomerId(Guid customerId);
    }
}
