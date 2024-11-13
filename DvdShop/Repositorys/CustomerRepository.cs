using DvdShop.Database;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly DvdStoreContext _dbcontext;

        public CustomerRepository(DvdStoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


       
    }
}
