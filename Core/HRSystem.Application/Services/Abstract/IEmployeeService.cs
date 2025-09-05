using HRSystem.Application.DTOs.Employees;

namespace HRSystem.Application.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<List<ResultEmployeeDTO>> GetAllAsync();
        Task<ResultEmployeeDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateEmployeeDTO dto);
        Task<bool> UpdateAsync(UpdateEmployeeDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
