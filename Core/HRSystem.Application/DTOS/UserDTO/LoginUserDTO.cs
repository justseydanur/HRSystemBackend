using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOS.UserDTO
{
    public class LoginUserDTO
    {
        public string FullName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string Password { get; set; } = null!;
    
    }
}
