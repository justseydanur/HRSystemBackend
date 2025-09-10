using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOS.TokenDTO
{
    public class TokenResponseDTO
    {
        public required string accessToken { get; set; }
        public required string refreshToken { get; set; }

    }
}
