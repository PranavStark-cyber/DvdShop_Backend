using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePayment(Payment payment);
        Task<Payment> GetPaymentByReferenceId(string referenceId);
    }
}
