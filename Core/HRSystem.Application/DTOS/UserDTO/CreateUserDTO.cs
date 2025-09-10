using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOS.UserDTO
{
    public class CreateUserDTO
    {
        public int id { get; set; }
        public string firstName { get; set; } = null!;
        public string lastName { get; set; }

        public string tckimlik { get; set; } = null!;
        public DateTime? dogumTarihi { get; set; } = null!;
        public string telNo { get; set; } = null!;
        public string email { get; set; } = null!;
        public string personnelPhoto { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string adres { get; set; } = null!;

    }

}
