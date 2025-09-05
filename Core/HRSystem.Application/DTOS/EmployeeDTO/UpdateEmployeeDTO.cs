using System;

namespace HRSystem.Application.DTOs.Employees
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string TCKimlik { get; set; } = null!;
        public DateTime DogumTarihi { get; set; }
        public string TelNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string WorkingStatus { get; set; } = null!;
        public string PersonnelPhoto { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public int TotalLeave { get; set; }
        public int UsedLeave { get; set; }
        public string? Adres { get; set; }
        public int DepartmentId { get; set; }
    }
}
