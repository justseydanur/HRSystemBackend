using HRSystem.Application.DTOS.TokenDTO;
using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Services.Abstract
{
    public interface ITokenServices
    {
        string CreateToken(User user);
        Task<TokenResponseDTO> CreateTokenResponse(User? user);
        Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO request);
    }
}
