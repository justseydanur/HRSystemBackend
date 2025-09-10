using HRSystem.Application.DTOS.UserDTO;
using HRSystem.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HRSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenServices _tokenService;

        public UserController(IUserService userService, ITokenServices tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO dto)
        {
            var result = await _userService.CreateUserAsync(dto);
            if (result == null)
                return BadRequest("Kullanıcı oluşturulamadı.");
            
            return Ok(result);
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var user = await _userService.LoginUserAsync(dto);
            if (user == null)
                return Unauthorized("Email veya şifre hatalı.");
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers([FromQuery] string department, [FromQuery] string position)
        {
            IEnumerable<ResultUserDTO> users;

            if (!string.IsNullOrEmpty(department) || !string.IsNullOrEmpty(position))
                users = await _userService.FilterUsersAsync(department, position);
            else
                users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok(user);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto)
        {
            var updatedUser = await _userService.UpdateUserAsync(dto);
            if (updatedUser == null)
                return NotFound("Güncellenecek kullanıcı bulunamadı.");

            return Ok(updatedUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var deleted = await _userService.DeleteUserAsync(email);
            if (!deleted)
                return NotFound("Silinecek kullanıcı bulunamadı.");

            return Ok("Kullanıcı başarıyla silindi.");
        }

        [Authorize]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterUsers([FromQuery] string? department = null, [FromQuery] string? position = null)
        {
            var users = await _userService.FilterUsersAsync(department, position);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string query)
        {
            var users = await _userService.SearchUsersAsync(query);
            return Ok(users);
        }
    }
}
