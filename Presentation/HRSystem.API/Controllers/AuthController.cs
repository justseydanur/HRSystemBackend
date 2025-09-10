using HRSystem.Application.DTOS.TokenDTO;
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

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDTO>> RefreshToken(RefreshTokenRequestDTO request)
        {
            var result = await _tokenService.RefreshTokensAsync(request);
            if (result is null || result.accessToken is null || result.refreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }


    }
}
