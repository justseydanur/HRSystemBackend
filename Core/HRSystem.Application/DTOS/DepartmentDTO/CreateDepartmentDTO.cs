namespace HRSystem.Application.DTOs.Departments
{
    public class CreateDepartmentDTO
    {
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
