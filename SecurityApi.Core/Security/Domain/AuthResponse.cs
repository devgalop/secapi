using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Core.Security.Domain
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public Token? Result { get; set; }

        public AuthResponse(bool isSuccess, string? error = "", Token? token = null )
        {
            IsSuccess = isSuccess;
            ErrorMessage = error;
            Result = token;
        }
    }
}
