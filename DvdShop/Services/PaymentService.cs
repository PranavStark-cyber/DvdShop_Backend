using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;

namespace DvdShop.Services
{
    public class PaymentService: IPaymentService
    {

        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> ProcessPayment(Rental rental)
        {
            int rentalDays = (rental.ReturnDate.Value - rental.RentalDate).Days;
            decimal amount = rentalDays * rental.DVD.Price;

            var payment = new Payment
            {
                ReferenceId = Guid.NewGuid().ToString(),
                Amount = amount,
                CustomerId = rental.CustomerId
            };

            return await _paymentRepository.CreatePayment(payment);
        }
    }
}
