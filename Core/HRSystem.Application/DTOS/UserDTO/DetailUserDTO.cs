using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOS.UserDTO
{
    public class DetailUserDTO
    {
        public int? id { get; set; }
        public string FullName { get; set; } = null!;
        public string tckimlik { get; set; } = null!;
        public DateTime? dogumTarihi { get; set; }
        public string email { get; set; } = null!;
        public string departmentId { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public string position { get; set; } = null!;
    }
}
