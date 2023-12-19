using SecurityApi.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Infrastructure.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindUserAsync(string username);
    }
}
