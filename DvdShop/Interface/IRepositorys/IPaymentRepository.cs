using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePayment(Payment payment);
        Task<Payment> GetPaymentByReferenceId(Guid Id);
        Task<List<Payment>> GetPaymentsByCustomerId(Guid customerId);
        decimal GetTotalEarnings();
    }
}
