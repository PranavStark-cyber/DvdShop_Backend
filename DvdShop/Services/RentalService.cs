using DvdShop.DTOs.Requests.Rental;
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
        private readonly IWhatsAppServices _whatsAppServices;
        private readonly ICustomerRepository _customerRepository;
        private readonly IManagerRepository _managerRepository;

        public RentalService(IRentalRepository rentalRepository, INotificationRepository notificationRepository,
                         IInventoryRepository inventoryRepository, IPaymentService paymentService,IWhatsAppServices whatsAppServices,ICustomerRepository customerRepository,IManagerRepository managerRepository)
    {
        _rentalRepository = rentalRepository;
        _notificationRepository = notificationRepository;
        _inventoryRepository = inventoryRepository;
        _paymentService = paymentService;
            _whatsAppServices = whatsAppServices;
            _customerRepository = customerRepository;
            _managerRepository= managerRepository;

            
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

                // Send WhatsApp notification
                var customerPhoneNumber = rental.Customer.PhoneNumber; // Replace with actual property for customer's phone number
                if (!string.IsNullOrEmpty(customerPhoneNumber))
                {
                    var message = $"Your rental for '{rental.DVD.Title}' has been approved. Enjoy your movie!";
                    await _whatsAppServices. SendWhatsAppNotification(customerPhoneNumber, message);
                }
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
        public async Task RequestRental(CreateRentalDto createRentalDto)
        {
            // Fetch the customer to validate
            var customer = await _customerRepository.GetCustomerById(createRentalDto.CustomerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID '{createRentalDto.CustomerId}' not found.");
            }

            // Fetch the DVD by ID to include in the rental object
            var dvd = await _managerRepository.GetDvdById(createRentalDto.DvdId);
            if (dvd == null)
            {
                throw new KeyNotFoundException($"DVD with ID '{createRentalDto.DvdId}' not found.");
            }

            // Proceed with rental creation
            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                DvdId = createRentalDto.DvdId,
                CustomerId = createRentalDto.CustomerId,
                RentalDays = createRentalDto.RentalDays,
                Status = RentalStatus.Request,
                RequestDate = DateTime.UtcNow,
                DVD = dvd // Make sure the DVD is assigned to the rental
            };

            // Decrease available copies of the DVD
            await _inventoryRepository.UpdateInventory(createRentalDto.DvdId, -createRentalDto.CopySofDvd);

            // Save rental to repository
            await _rentalRepository.CreateRental(rental);

            // Add notification for the customer
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
