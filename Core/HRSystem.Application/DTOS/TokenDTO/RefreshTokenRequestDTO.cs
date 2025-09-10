using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOS.TokenDTO
{
    public class RefreshTokenRequestDTO
    {
        public int UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
