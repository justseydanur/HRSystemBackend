using HRSystem.Application.DTOS.UserDTO;
using HRSystem.Application.Services.Abstract;
using HRSystem.Domain.Entities;
using HRSystem.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System;

namespace HRSystem.Application.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultUserDTO> CreateUserAsync(CreateUserDTO dto)
        {
            using var hmac = new HMACSHA512();
            var user = new User
            {
                FullName = dto.FullName,
                TcNo = dto.TcNo,
                BirthDate = dto.BirthDate,
                EmployeeNumber = dto.EmployeeNumber,
                email = dto.email,
                Department = dto.Department,
                Position = dto.Position,
                PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password))),
                PasswordSalt = Convert.ToBase64String(hmac.Key)
            };

            await _userRepository.AddAsync(user);

            return new ResultUserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.email,
                Department = user.Department,
                Position = user.Position
            };
        }

        public async Task<DetailUserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null!; // null! => warning'i susturur

            return new DetailUserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                TcNo = user.TcNo,   
                BirthDate = user.BirthDate,
                EmployeeNumber = user.EmployeeNumber,
                email = user.email,
                Department = user.Department,
                Position = user.Position
            };
        }

        public async Task<IEnumerable<ResultUserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new ResultUserDTO
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.email,
                Department = u.Department,
                Position = u.Position
            });
        }

        public async Task<ResultUserDTO?> UpdateUserAsync(UpdateUserDTO dto)
        {
            if (!dto.Id.HasValue) return null!; // Id null ise hiç işlem yapma

            var user = await _userRepository.GetByIdAsync(dto.Id.Value); // burada .Value kullandık
            if (user == null) return null!;

            user.FullName = dto.FullName;
            user.TcNo = dto.TcNo;
            user.BirthDate = dto.BirthDate;
            user.EmployeeNumber = dto.EmployeeNumber;
            user.email = dto.Email;
            user.Department = dto.Department;
            user.Position = dto.Position;

            await _userRepository.UpdateAsync(user);

            return new ResultUserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.email,
                Department = user.Department,
                Position = user.Position
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user);
            return true;
        }

        // Filter ve Search metodları
        public async Task<IEnumerable<ResultUserDTO>> FilterUsersAsync(string department, string position)
        {
            var users = await _userRepository.GetAllAsync();
            var filtered = users.Where(u =>
                (string.IsNullOrEmpty(department) || u.Department == department) &&
                (string.IsNullOrEmpty(position) || u.Position == position)
            );

            return filtered.Select(u => new ResultUserDTO
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.email,
                Department = u.Department,
                Position = u.Position
            });
        }

        public async Task<IEnumerable<ResultUserDTO>> SearchUsersAsync(string query)
        {
            var users = await _userRepository.GetAllAsync();
            var searched = users.Where(u =>
                (!string.IsNullOrEmpty(u.FullName) && u.FullName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(u.email) && u.email.Contains(query, StringComparison.OrdinalIgnoreCase))
            );

            return searched.Select(u => new ResultUserDTO
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.email,
                Department = u.Department,
                Position = u.Position
            });
        }

        public async Task<ResultUserDTO?> LoginUserAsync(LoginUserDTO dto)
        {
            var user = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.email == dto.Email);
            if (user == null) return null!;

            using var hmac = new HMACSHA512(Convert.FromBase64String(user.PasswordSalt));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            if (Convert.ToBase64String(computedHash) != user.PasswordHash)
                return null!;

            return new ResultUserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.email,
                Department = user.Department,
                Position = user.Position,
                Role = user.Role
            };
        }
        public async Task<ResultUserDTO> RegisterAsync(CreateUserDTO dto)
        {
            using var hmac = new HMACSHA512();
            var user = new User
            {
                FullName = dto.FullName,
                TcNo = dto.TcNo,
                BirthDate = dto.BirthDate,
                EmployeeNumber = dto.EmployeeNumber,
                email = dto.email,
                Department = dto.Department,
                Position = dto.Position,
                PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password))),
                PasswordSalt = Convert.ToBase64String(hmac.Key)
            };

            await _userRepository.AddAsync(user);

            return new ResultUserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.email,
                Department = user.Department,
                Position = user.Position
            };
        }


    }
}

//   Açıklamalar:
//GetAllAsync() → tüm kullanıcıları alıyoruz ve FirstOrDefault ile username’e göre filtreliyoruz.
//Not: Küçük projelerde sorun olmaz, ama büyük veri için repository’ye GetByUsernameAsync gibi bir metot eklemek daha doğru olur.
//Hash ve salt ile parola doğrulama yapılır.
//Eğer kullanıcı bulunamaz veya şifre yanlışsa null dönüyoruz(dilersen özel exception da fırlatabilirsin).
//Şifreler hiçbir zaman DTO veya API response’da dönmez.