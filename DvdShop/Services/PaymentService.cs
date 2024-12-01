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

        //public async Task<Payment> ProcessPayment(Rental rental)
        //{
        //    int rentalDays = (rental.ReturnDate.Value - rental.ApprovedDate.Value).Days;
        //    decimal amount = rentalDays * rental.DVD.Price;

        //    var payment = new Payment
        //    {
        //        ReferenceId = Guid.NewGuid().ToString(),
        //        Amount = amount,
        //        CustomerId = rental.CustomerId
        //    };

        //    return await _paymentRepository.CreatePayment(payment);
        //}


        public async Task<Payment> ProcessPayment(Rental rental)
{
 
    int rentalDays = (rental.ReturnDate.Value - rental.ApprovedDate.Value).Days;

 
    decimal overdueFeeRate = 10.00m;
    decimal amount = 0m;


    if (rentalDays <= rental.RentalDays)
    {
        amount = rentalDays * rental.DVD.Price;
    }
    else
    {
        // Calculate normal rental amount for approved days
        amount = rental.RentalDays * rental.DVD.Price;


        int overdueDays = rentalDays - rental.RentalDays;

        decimal overdueFee = overdueDays * overdueFeeRate;

        amount += overdueFee;
    }

    var payment = new Payment
    {
        ReferenceId = Guid.NewGuid().ToString(),
        Amount = amount,
        CustomerId = rental.CustomerId
    };

    // Create and return the payment via repository
    return await _paymentRepository.CreatePayment(payment);
}

    }
}
