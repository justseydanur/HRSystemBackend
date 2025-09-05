using System;

namespace HRSystem.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }                 // id
        public string FirstName { get; set; } = null!;  // firstName
        public string LastName { get; set; } = null!;   // lastName
        public string TCKimlik { get; set; } = null!;   // tckimlik
        public DateTime DogumTarihi { get; set; }       // dogumTarihi
        public string TelNo { get; set; } = null!;      // telNo
        public string Email { get; set; } = null!;      // email
        public string Position { get; set; } = null!;   // position
        public string WorkingStatus { get; set; } = null!; // workingStatus
        public string PersonnelPhoto { get; set; } = null!; // personnelPhoto
        public DateTime StartDate { get; set; }         // startDate
        public int TotalLeave { get; set; }             // totalLeave
        public int UsedLeave { get; set; }              // usedLeave
        public string? Adres { get; set; }              // adres (opsiyonel)

        // İlişki
        public int DepartmentId { get; set; }           // departmentId
        public Department Department { get; set; } = null!; // departmentName
    }
}
