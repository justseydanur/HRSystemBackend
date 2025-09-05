using HRSystem.Application.DTOs.Departments;
using HRSystem.Application.Services.Abstract;
using HRSystem.Domain.Entities;
using HRSystem.Application.Interfaces; // IGenericRepository<T> burada

namespace HRSystem.Application.Services.Concrete
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepository;

        public DepartmentService(IGenericRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<ResultDepartmentDTO>> GetAllAsync()
        {
            var list = await _departmentRepository.GetAllAsync();
            return list
                .OrderBy(x => x.Name)
                .Select(x => new ResultDepartmentDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                })
                .ToList();
        }

        public async Task<ResultDepartmentDTO?> GetByIdAsync(int id)
        {
            var d = await _departmentRepository.GetByIdAsync(id);
            if (d == null) return null;

            return new ResultDepartmentDTO
            {
                Id = d.Id,
                Name = d.Name,
                IsActive = d.IsActive
            };
        }

        public async Task<int> CreateAsync(CreateDepartmentDTO dto)
        {
            var entity = new Department
            {
                Name = dto.Name.Trim(),
                IsActive = dto.IsActive
            };
            await _departmentRepository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(UpdateDepartmentDTO dto)
        {
            var entity = await _departmentRepository.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            entity.Name = dto.Name.Trim();
            entity.IsActive = dto.IsActive;

            await _departmentRepository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _departmentRepository.GetByIdAsync(id);
            if (entity == null) return false;

            await _departmentRepository.DeleteAsync(entity);
            return true;
        }
    }
}
