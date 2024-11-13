using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByNameAsync(string roleName);  // Get a role by its name
        Task AddRoleAsync(Role role);                    // Add a new role to the database
        Task<List<Role>> GetAllRolesAsync();            // Optional: Get all roles from the database
    }

}
