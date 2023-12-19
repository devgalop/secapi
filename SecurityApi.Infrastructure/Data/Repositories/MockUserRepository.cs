using SecurityApi.Infrastructure.Data.Interfaces;
using SecurityApi.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Infrastructure.Data.Repositories
{
    public class MockUserRepository : IUserRepository
    {
        private List<User> _users;
        public MockUserRepository()
        {
            _users = new List<User>() 
            {
                new User("user1", "1234", "employee"),
                new User("user3", "1234", "employee"),
                new User("user.admin", "1234", "admin"),
                new User("user2", "1234", "employee")
            };
        }

        public async Task<User?> FindUserAsync(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }
    }
}
