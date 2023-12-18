using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Core.Security.Domain
{
    public class AuthRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public AuthRequest(string userName, string password)
        {
            Username = userName;
            Password = password;
        }
    }
}
