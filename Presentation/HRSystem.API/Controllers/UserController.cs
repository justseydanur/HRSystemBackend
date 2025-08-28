using HRSystem.Application.Interfaces;
using HRSystem.Application.DTOS.UserDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HRSystem.Application.Services.Abstract; // JWT için gerekli

namespace HRSystem.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        // DI ile IUserService inject edildi
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/User/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO dto)
        {
            var result = await _userService.CreateUserAsync(dto);
            if (result == null)
                return BadRequest("Kullanıcı oluşturulamadı.");

            return Ok(result);
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var token = await _userService.LoginUserAsync(dto);

            if (token == null)
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");

            return Ok(new { Token = token });
        }

        // GET: api/User/all
        [HttpGet("all")]
        [Authorize] // Sadece JWT token geçerli kullanıcılar erişebilir
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync(); // Corrected method call
            return Ok(users);
        }
    }
}

//Açıklamalar
//[ApiController] ve[Route] → Controller’ı API olarak tanımlar ve route ayarlar
//DI ile Service → Constructor üzerinden IUserService inject edilir
//Register endpoint → CreateUserAsync metodunu çağırır
//Login endpoint → LoginUserAsync metodunu çağırır
//Status kodları → Başarılı Ok(), başarısız BadRequest() veya Unauthorized()
//Açıklamalar:
//CreateUserAsync → register işlemi, şifre hash + salt ile saklanır, API’ye ResultUserDTO döner
//LoginUserAsync → login işlemi, şifre doğrulama + JWT token üretir, token API’ye döner
//Endpoint’ler artık güvenli ve standart HTTP status kodları ile çalışıyor:
//Register başarısız → BadRequest()
//Login başarısız → Unauthorized()
//Başarılı → Ok()