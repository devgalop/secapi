using SecurityApi.Core.Security.Domain;
using SecurityApi.Core.Security.Interfaces;
using SecurityApi.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Core.Security.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUser(string username) 
        {
            var userFound = await _userRepository.FindUserAsync(username);
            if (userFound != null) 
            {
                return new()
                {
                    Username = userFound.Username,
                    Role = userFound.Role
                };
            }
            return null;
        }
    }
}
