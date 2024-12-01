using Azure.Core;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DvdShop.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<ICollection<Customer>> GetCustomers()
        {
            return await _customerRepository.GetCustomers();
        }

        public async Task<Customer?> GetCustomerById(Guid customerId)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(customerId);
                return customer;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the customer.", ex);
            }
        }



        public async Task<Customer> UpdateCustomer(UpdateCustomerDTO customerDto)
        {
            var customer = await _customerRepository.GetCustomerById(customerDto.Id);

            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            // Update Customer details
            customer.Nic = customerDto.Nic;
            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.PhoneNumber = customerDto.PhoneNumber;

            // Handle Address logic
            if (customerDto.Address != null)
            {
                if (customer.Address == null)
                {
                    // If no address exists, create a new one
                    customer.Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = customer.Id,
                        Street = customerDto.Address.Street,
                        City = customerDto.Address.City,
                        Country = customerDto.Address.Country
                    };
                }
                else
                {
                    // Update existing address
                    customer.Address.Street = customerDto.Address.Street;
                    customer.Address.City = customerDto.Address.City;
                    customer.Address.Country = customerDto.Address.Country;
                }
            }

            // Persist the changes
            var isUpdated = await _customerRepository.UpdateCustomer(customer);
            if (!isUpdated)
            {
                throw new InvalidOperationException("No changes were made to the customer.");
            }

            return customer;
        }


        public async Task<bool> DeleteCustomerAsync(Guid customerId)
        {
            try
            {
                return await _customerRepository.DeleteCustomer(customerId);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("Customer not found.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Cannot delete customer with active rentals.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the customer.", ex);
            }
        }

        public async Task<bool> AddReviewAsync(AddReviewDTO reviewDto)
        {
            // Validation for rating
            if (reviewDto.Rating < 1 || reviewDto.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            // Check if Customer exists using repository
            if (!await _customerRepository.CustomerExistsAsync(reviewDto.CustomerId))
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            // Check if DVD exists using repository
            if (!await _customerRepository.DVDExistsAsync(reviewDto.DvdId))
            {
                throw new KeyNotFoundException("DVD not found.");
            }

            // Create Review entity
            var review = new Review
            {
                CustomerId = reviewDto.CustomerId,
                DvdId = reviewDto.DvdId,
                ReviewDate = DateTime.Now,
                Comment = reviewDto.Comment,
                Rating = reviewDto.Rating
            };

            // Save Review using repository
            return await _customerRepository.AddReviewAsync(review);
        }

    }
}
