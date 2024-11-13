using DvdShop.Database;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DvdStoreContext _context;

        public RoleRepository(DvdStoreContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.Where(r => r.Name.ToLower() == roleName.ToLower()).FirstOrDefaultAsync();
        }


        // Add a new role to the database
        public async Task AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        // Optional: Get all roles
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }

}
