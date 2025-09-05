using System.ComponentModel.DataAnnotations;

namespace HRSystem.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        // İleride Employee ilişkilendireceksen:
        // public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
