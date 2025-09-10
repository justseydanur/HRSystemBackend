using HRSystem.Application.DTOS.TokenDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOS.UserDTO
{
    public class ResultUserDTO
    {
        public string accesToken { get; set; } = null!;
        public string refreshToken { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string email { get; set; } = null!;
    
    }
}
