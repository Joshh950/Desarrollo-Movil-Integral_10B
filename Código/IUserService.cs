using loginv3.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace loginv3.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User user);
        Task<User?> LoginAsync(string email, string password);
        Task<User?> GetAuthenticatedUserAsync();
        Task<List<User>> GetUsersAsync();
        Task DeleteUserAsync(string id);
        Task UpdateUserAsync(User user);
    }
}
