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

        public async Task<Payment> GetPaymentByReferenceId(string referenceId)
        {
            return await _paymentcontext.Payments.FirstOrDefaultAsync(p => p.ReferenceId == referenceId);
        }
    }
}
