using DvdShop.Database;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly DvdStoreContext _paymentcontext;

        public PaymentRepository(DvdStoreContext paymentcontext)
        {
            _paymentcontext = paymentcontext;
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            _paymentcontext.Payments.Add(payment);
            await _paymentcontext.SaveChangesAsync();
            return payment;
        }

        public decimal GetTotalEarnings()
        {
            // Sum the Amount for all payments in the database
            var totalEarnings = _paymentcontext.Payments.Sum(p => p.Amount);
            return totalEarnings;
        }
        public async Task<Payment> GetPaymentByReferenceId(Guid Id)
        {
            return await _paymentcontext.Payments.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<List<Payment>> GetPaymentsByCustomerId(Guid customerId)
        {
            return await _paymentcontext.Payments
                                 .Where(p => p.ReferenceId == customerId)
                                 .ToListAsync();
        }

    }
}
