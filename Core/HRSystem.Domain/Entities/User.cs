using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Domain.Entities
{
    public class User
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string tckimlik { get; set; }
        public DateTime? dogumTarihi { get; set; }
        public string telNo { get; set; } = null!;     
        public string email { get; set; } = null!;
        public string? departmentId { get; set; } = null!;
        public string? departmentName { get; set; } = null!;
        public int? totalLeave { get; set; }
        public int? usedLeave { get; set; }
        public string? position { get; set; } = null!;
        public string? workingStatus { get; set; } = null!;
        public string? personnelPhoto { get; set; } = null!;
        public DateTime? startDate { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public string? Role { get; set; } = null!;

        public string? adres { get; set; } = null!;

        public int? salary { get; set; } = null!;
        public int? mealCost { get; set; } = null!;
        public int? transportCost { get; set; } = null!;
        public int? otherCost { get; set; } = null!;

        public string? RefreshToken { get; set; } = null!;
        public DateTime? RefreshTokenExpiryTime { get; set; } = null!;


    }

}
