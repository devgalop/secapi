using SecurityApi.Core.Security.Domain;
using SecurityApi.Core.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Core.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtFactoryService _jwtFactoryService;

        public AuthService(IJwtFactoryService jwtFactoryService)
        {
            _jwtFactoryService = jwtFactoryService;
        }

        public async Task<AuthResponse> Login(AuthRequest request) 
        {
            try
            {
                if (request == null) 
                {
                    return new(false, error: "El modelo es requerido");
                }
                if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                {
                    return new(false, error: "El usuario y/o contraseña son requeridos");
                }
                //Add here the logic to validate the user
                var tokenGenerated = _jwtFactoryService.GenerateToken(new()
                {
                    Username = request.Username,
                    Password = request.Password
                });
                return new(true, token: tokenGenerated);
            }
            catch (Exception ex)
            {
                return new(false, error: ex.ToString());
            }
        }
    }
}
