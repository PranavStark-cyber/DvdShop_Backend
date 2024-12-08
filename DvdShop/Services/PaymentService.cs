using DvdShop.DTOs.Responses.Customers;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;
using DvdShop.Repositorys;

namespace DvdShop.Services
{
    public class PaymentService: IPaymentService
    {

        private readonly IPaymentRepository _paymentRepository;
        private readonly IRentalRepository _personalRepository;

        public PaymentService(IPaymentRepository paymentRepository, IRentalRepository personalRepository)
        {
            _paymentRepository = paymentRepository;
            _personalRepository = personalRepository;
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









        public async Task<OverdueAmountResponse> GetOverdueAmountByCustomerId(Guid customerId)
        {
            try
            {
                // Fetch all rentals for the given customer
                var rentals = await _personalRepository.GetRentalsByCustomerId(customerId);

                // Get today's date for overdue calculation
                DateTime today = DateTime.UtcNow;

                // Calculate overdue amount by checking if the rental is overdue
                decimal overdueAmount = rentals
                    .Where(r => r.ApprovedDate.HasValue &&
                                r.ApprovedDate.Value.AddDays(r.RentalDays) < today &&
                                r.Status != RentalStatus.Returned) // Status should not be "Returned"
                    .Sum(r => r.DVD.Price); // Assuming each rental has a price defined in the DVD entity

                return new OverdueAmountResponse
                {
                    CustomerId = customerId,
                    OverdueAmount = overdueAmount
                };
            }
            catch (Exception ex)
            {
                // Handle exception (log it or rethrow as needed)
                throw new ApplicationException("Error calculating overdue amount", ex);
            }
        }



        public async Task<TotalAmountResponse> GetTotalAmountByCustomerId(Guid customerId)
        {
            try
            {
                // Fetch all payments for the given customer
                var payments = await _paymentRepository.GetPaymentsByCustomerId(customerId);

                // Calculate total amount
                var totalAmount = payments.Sum(p => p.Amount);

                return new TotalAmountResponse
                {
                    CustomerId = customerId,
                    TotalAmount = totalAmount
                };
            }
            catch (Exception ex)
            {
                // Handle exception (log it or rethrow as needed)
                throw new ApplicationException("Error calculating total amount", ex);
            }
        }


    }
}
