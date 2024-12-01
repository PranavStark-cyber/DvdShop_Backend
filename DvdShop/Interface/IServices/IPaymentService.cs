using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPayment(Rental rental);
    }
}
