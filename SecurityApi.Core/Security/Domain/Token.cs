using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Core.Security.Domain
{
    public class Token
    {
        public string? AuthToken { get; set; }
        public DateTime Expiration { get; set; }

        public Token(string token, DateTime expiration)
        {
            AuthToken = token;
            Expiration = expiration;
        }
    }
}
