using loginv3.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace loginv3.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private User? _authenticatedUser;

        public UserService(IMongoClient client)
        {
            var database = client.GetDatabase("UserDB");
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User> RegisterAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
           
            _authenticatedUser = await _users.Find(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();

            
            if (_authenticatedUser != null)
            {
                
                return _authenticatedUser;
            }

            return null; 
        }


        public async Task<User?> GetAuthenticatedUserAsync()
        {
            return _authenticatedUser;
        }

        public bool IsAuthenticated()
        {
            return _authenticatedUser != null;
        }

        public async Task LogoutAsync()
        {
            _authenticatedUser = null; 
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task DeleteUserAsync(string id)
        {
            await _users.DeleteOneAsync(u => u.Id == id);
            _authenticatedUser = null;
        }

        public async Task UpdateUserAsync(User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

    }
}
