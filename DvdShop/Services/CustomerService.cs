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


        public async Task<Customer> RegisterCustomer(RegisterDTO registerDTO)
        {


            var register = new Customer()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                Mobilenumber = registerDTO.Mobilenumber,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                Nic = registerDTO.Nic,
                Address = registerDTO.Address != null ? registerDTO.Address.Select(a => new Address
                {
                    Street = a.Street,
                    City = a.City,
                    State = a.State,
                    Zipcode = a.Zipcode,
                    Country = a.Country,
                }).ToList() : new List<Address>()

            };

           var data =  await _customerRepository.RegisterCustomer(register);
            var res = new Customer
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Mobilenumber = data.Mobilenumber,
                Password = data.Password,
                Nic = data.Nic,
                Address = data.Address,
            };
            return res;
        }

        public async Task<String> Login(LoginRequestDTO request)
        {
            var userDetails = await _customerRepository.Login(request);


            return("Successfully login");
        }
    }
}
