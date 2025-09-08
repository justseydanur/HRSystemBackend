using HRSystem.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenServices _tokenService;

        public AuthController(ITokenServices tokenService)
        {
            _tokenService = tokenService;
        }

        public record LoginRequest(string Email, string Password);

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            // şimdilik fake kontrol (ileride DB’den)
            if (req.Email == "admin@hr.com" && req.Password == "Admin123!")
            {
                var token = _tokenService.CreateToken(1, req.Email, "Admin");
                return Ok(new { token });
            }

            return Unauthorized(new { message = "Geçersiz kullanıcı" });
        }
    }
}
