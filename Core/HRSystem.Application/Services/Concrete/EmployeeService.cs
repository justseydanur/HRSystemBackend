using HRSystem.Application.DTOs.Employees;
using HRSystem.Application.Services.Abstract;
using HRSystem.Domain.Entities;
using HRSystem.Application.Interfaces; // IGenericRepository<T> burada

namespace HRSystem.Application.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _employeeRepository;

        public EmployeeService(IGenericRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<ResultEmployeeDTO>> GetAllAsync()
        {
            var list = await _employeeRepository.GetAllAsync();
            return list.Select(e => new ResultEmployeeDTO
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                TCKimlik = e.TCKimlik,
                DogumTarihi = e.DogumTarihi,
                TelNo = e.TelNo,
                email = e.email,
                Position = e.Position,
                WorkingStatus = e.WorkingStatus,
                PersonnelPhoto = e.PersonnelPhoto,
                StartDate = e.StartDate,
                TotalLeave = e.TotalLeave,
                UsedLeave = e.UsedLeave,
                Adres = e.Adres,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.Name ?? ""
            }).ToList();
        }

        public async Task<ResultEmployeeDTO?> GetByIdAsync(int id)
        {
            var e = await _employeeRepository.GetByIdAsync(id);
            if (e == null) return null;

            return new ResultEmployeeDTO
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                TCKimlik = e.TCKimlik,
                DogumTarihi = e.DogumTarihi,
                TelNo = e.TelNo,
                email = e.email,
                Position = e.Position,
                WorkingStatus = e.WorkingStatus,
                PersonnelPhoto = e.PersonnelPhoto,
                StartDate = e.StartDate,
                TotalLeave = e.TotalLeave,
                UsedLeave = e.UsedLeave,
                Adres = e.Adres,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.Name ?? ""
            };
        }

        public async Task<int> CreateAsync(CreateEmployeeDTO dto)
        {
            var entity = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                TCKimlik = dto.TCKimlik,
                DogumTarihi = dto.DogumTarihi,
                TelNo = dto.TelNo,
                email = dto.email,
                Position = dto.Position,
                WorkingStatus = dto.WorkingStatus,
                PersonnelPhoto = dto.PersonnelPhoto,
                StartDate = dto.StartDate,
                TotalLeave = dto.TotalLeave,
                UsedLeave = dto.UsedLeave,
                Adres = dto.Adres,
                DepartmentId = dto.DepartmentId
            };
            await _employeeRepository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(UpdateEmployeeDTO dto)
        {
            var entity = await _employeeRepository.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.TCKimlik = dto.TCKimlik;
            entity.DogumTarihi = dto.DogumTarihi;
            entity.TelNo = dto.TelNo;
            entity.email = dto.email;
            entity.Position = dto.Position;
            entity.WorkingStatus = dto.WorkingStatus;
            entity.PersonnelPhoto = dto.PersonnelPhoto;
            entity.StartDate = dto.StartDate;
            entity.TotalLeave = dto.TotalLeave;
            entity.UsedLeave = dto.UsedLeave;
            entity.Adres = dto.Adres;
            entity.DepartmentId = dto.DepartmentId;

            await _employeeRepository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _employeeRepository.GetByIdAsync(id);
            if (entity == null) return false;

            await _employeeRepository.DeleteAsync(entity);
            return true;
        }
    }
}
