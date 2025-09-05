using HRSystem.Application.DTOs.Departments;

namespace HRSystem.Application.Services.Abstract
{
    public interface IDepartmentService
    {
        Task<List<ResultDepartmentDTO>> GetAllAsync();
        Task<ResultDepartmentDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateDepartmentDTO dto);
        Task<bool> UpdateAsync(UpdateDepartmentDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
