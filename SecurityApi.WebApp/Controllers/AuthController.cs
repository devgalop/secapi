using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityApi.Core.Security.Domain;
using SecurityApi.Core.Security.Interfaces;

namespace SecurityApi.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            try
            {
                var response = await _authService.Login(request);
                if (!response.IsSuccess)
                {
                    return NotFound();
                }
                return Ok(response.Result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser() 
        {
            return Ok();
        }
    }
}
