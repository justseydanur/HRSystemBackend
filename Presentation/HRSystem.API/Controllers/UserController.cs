using HRSystem.Application.DTOS.UserDTO;
using HRSystem.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace HRSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO dto)
        {
            var result = await _userService.CreateUserAsync(dto);
            if (result == null)
                return BadRequest("Kullanıcı oluşturulamadı.");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var result = await _userService.LoginUserAsync(dto);
            if (result == null)
                return Unauthorized("Email veya şifre hatalı.");

            return Ok(result);
        }

        [HttpGet("all")]
        //[Authorize]
        public async Task<IActionResult> GetAllUsers([FromQuery] string department, [FromQuery] string position)
        {
            IEnumerable<ResultUserDTO> users;

            if (!string.IsNullOrEmpty(department) || !string.IsNullOrEmpty(position))
                users = await _userService.FilterUsersAsync(department, position);
            else
                users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }


        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPut("update")]
        //[Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto)
        {
            var updatedUser = await _userService.UpdateUserAsync(dto);
            if (updatedUser == null)
                return NotFound("Güncellenecek kullanıcı bulunamadı.");

            return Ok(updatedUser);
        }

        [HttpDelete("delete/{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound("Silinecek kullanıcı bulunamadı.");

            return Ok("Kullanıcı başarıyla silindi.");
        }

        [HttpGet("filter")]
        //[Authorize]
        public async Task<IActionResult> FilterUsers([FromQuery] string? department = null, [FromQuery] string? position = null)
        {
            var users = await _userService.FilterUsersAsync(department, position);
            return Ok(users);
        }

        [HttpGet("search")]
        //[Authorize]
        public async Task<IActionResult> SearchUsers([FromQuery] string query)
        {
            var users = await _userService.SearchUsersAsync(query);
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