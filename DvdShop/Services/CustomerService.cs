using Azure.Core;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;
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


        public async Task<Customer> UpdateCustomerAsync(UpdateCustomerDTO customerDto)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerDto.Id);

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
                        UserId = customer.Id,
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
            var isUpdated = await _customerRepository.UpdateCustomerAsync(customer);
            if (!isUpdated)
            {
                throw new InvalidOperationException("No changes were made to the customer.");
            }

            return customer;
        }
    }
}
