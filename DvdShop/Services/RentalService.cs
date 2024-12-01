using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;

namespace DvdShop.Services
{
    public class RentalService:IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
      private readonly INotificationRepository _notificationRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IPaymentService _paymentService;

    public RentalService(IRentalRepository rentalRepository, INotificationRepository notificationRepository,
                         IInventoryRepository inventoryRepository, IPaymentService paymentService)
    {
        _rentalRepository = rentalRepository;
        _notificationRepository = notificationRepository;
        _inventoryRepository = inventoryRepository;
        _paymentService = paymentService;
    }

    public async Task<IEnumerable<Rental>> GetAllRentals()
    {
        return await _rentalRepository.GetAllRentals();
    }

    public async Task<Rental> GetRentalById(Guid id)
    {
        return await _rentalRepository.GetRentalById(id);
    }

    public async Task ApproveRental(Guid id)
    {
        var rental = await _rentalRepository.GetRentalById(id);
        if (rental != null)
        {
                // Add notification to customer
                var notification = new Notification
                {
                    ReceiverId = rental.CustomerId,
                    Title = "Rental Approved",
                    Message = $"Your rental for {rental.DVD.Title} has been approved.",
                    ViewStatus = "Unread",  
                    Type = "Info",
                    Date = DateTime.UtcNow
                };


                await _notificationRepository.AddNotification(notification);

                var customerNotification = await _notificationRepository.GetNotificationById(notification.Id);
                if (customerNotification != null)
                {
                    customerNotification.ViewStatus = "Read";  
                    await _notificationRepository.UpdateNotification(customerNotification); 
                }


                // Decrease the available copies in the inventory
                await _inventoryRepository.UpdateInventory(rental.DvdId, -1);
        }
    }

    public async Task CollectRental(Guid id)
    {
        var rental = await _rentalRepository.GetRentalById(id);
        if (rental != null)
        {
            rental.Status = RentalStatus.Collected;
            rental.CollectedDate = DateTime.UtcNow;
            await _rentalRepository.UpdateRental(rental);

            await _notificationRepository.AddNotification(new Notification
            {
                ReceiverId = rental.CustomerId,
                Title = "Rental Collected",
                Message = $"Your rental for {rental.DVD.Title} has been collected.",
                ViewStatus = "Unread",
                Type = "Info",
                Date = DateTime.UtcNow
            });
        }
    }

    public async Task RequestRental(Rental rental)
    {
        rental.Status = RentalStatus.Request;
        rental.RequestDate = DateTime.UtcNow;
        await _rentalRepository.CreateRental(rental);

        await _notificationRepository.AddNotification(new Notification
        {
            ReceiverId = rental.CustomerId,
            Title = "Rental Requested",
            Message = $"You have requested to rent {rental.DVD.Title}.",
            ViewStatus = "Unread",
            Type = "Info",
            Date = DateTime.UtcNow
        });
    }

    public async Task ReturnRental(Guid id)
    {
        var rental = await _rentalRepository.GetRentalById(id);
        if (rental != null)
        {
            rental.Status = RentalStatus.Returned;
            rental.ReturnDate = DateTime.UtcNow;
            await _rentalRepository.UpdateRental(rental);

            // Calculate payment for the rental
            var payment = await _paymentService.ProcessPayment(rental);
            
            await _notificationRepository.AddNotification(new Notification
            {
                ReceiverId = rental.CustomerId,
                Title = "Rental Returned",
                Message = $"Your rental for {rental.DVD.Title} has been returned. Total payment: {payment.Amount}.",
                ViewStatus = "Unread",
                Type = "Info",
                Date = DateTime.UtcNow
            });

            // Update inventory to add back available copies
            await _inventoryRepository.UpdateInventory(rental.DvdId, 1);
        }
    }

    public async Task RejectRental(Guid id)
    {
        var rental = await _rentalRepository.GetRentalById(id);
        if (rental != null)
        {
            rental.Status = RentalStatus.Rejected;
            await _rentalRepository.UpdateRental(rental);


                // Add notification to customer
                var notification = new Notification
                {
                    ReceiverId = rental.CustomerId,
                    Title = "Rental Rejected",
                    Message = $"Your rental for {rental.DVD.Title} has been rejected.",
                    ViewStatus = "Unread",  
                    Type = "Warning",
                    Date = DateTime.UtcNow
                };

                await _notificationRepository.AddNotification(notification);


                var customerNotification = await _notificationRepository.GetNotificationById(notification.Id);
                if (customerNotification != null)
                {
                    customerNotification.ViewStatus = "Read";  // Change to "Read"
                    await _notificationRepository.UpdateNotification(customerNotification);  
                }
            }
    }


        public async Task<List<Rental>> GetRentalsByCustomerId(Guid customerId)
        {
            return (await _rentalRepository.GetRentalsByCustomerId(customerId)).ToList();
        }
    }
}
