using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOS.UserDTO
{
    public class CreateUserDTO
    {
        public string FullName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string TcNo { get; set; } = null!;
        public DateTime? BirthDate { get; set; } = null!;
        public string EmployeeNumber { get; set; } = null!;
        public string email { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Position { get; set; } = null!;
    }

}
