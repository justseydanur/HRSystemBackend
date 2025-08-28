using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOS.UserDTO
{
    public class CreateUserDTO
    {
        public string Username { get; set; }       // Kullanıcı adı
        public string Password { get; set; }       // Şifre (hashleme işlemi Service’de yapılacak)
        public string Email { get; set; }          // Email adresi
        public string Role { get; set; }           // Kullanıcı rolü (Admin, Employee vb.)
    }
}
