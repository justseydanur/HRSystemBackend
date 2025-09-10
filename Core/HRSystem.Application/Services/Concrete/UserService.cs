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
using Microsoft.AspNetCore.Identity;

namespace HRSystem.Application.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly ITokenServices _tokenservice;
        public UserService(IGenericRepository<User> userRepository, ITokenServices tokenservice)
        {
            _userRepository = userRepository;
            _tokenservice = tokenservice;

        }


        public async Task<ResultUserDTO?> LoginUserAsync(LoginUserDTO dto)
        {
            var user = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.email == dto.email);
            if (user == null) return null!;

            var saltBytes = Convert.FromBase64String(user.PasswordSalt);
            using var hmac = new HMACSHA512(saltBytes);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            if (Convert.ToBase64String(computedHash) != user.PasswordHash)
                return null!;


            var Token = await _tokenservice.CreateTokenResponse(user);

            return new ResultUserDTO
            {
                accesToken = Token.accessToken,
                refreshToken = Token.refreshToken,
                FullName = user.firstName + " " + user.lastName,
                email = user.email
            };
        }


        public async Task<ResultUserDTO> CreateUserAsync(CreateUserDTO dto)
        {
            using var hmac = new HMACSHA512();
            var user = new User
            {
                firstName = dto.firstName,
                lastName = dto.lastName,
                FullName = dto.firstName + " " + dto.lastName,
                tckimlik = dto.tckimlik,
                dogumTarihi = dto.dogumTarihi,
                telNo = dto.telNo,
                email = dto.email,
                adres = dto.adres,
                PasswordSalt = Convert.ToBase64String(hmac.Key),
                PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)))
            };

            await _userRepository.AddAsync(user);

            return new ResultUserDTO
            {
                accesToken = "",
                refreshToken = "",
                FullName = user.firstName + " " + user.lastName,
                email = user.email
            };
        }

        public async Task<DetailUserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null!; // null! => warning'i susturur

            return new DetailUserDTO
            {
                id = user.id,
                FullName = user.firstName + user.lastName,
                tckimlik = user.tckimlik,   
                dogumTarihi = user.dogumTarihi,
                email = user.email,
                departmentId = user.departmentId,
                DepartmentName = user.departmentName,
                position = user.position
            };
        }

        public async Task<IEnumerable<ResultUserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new ResultUserDTO
            {
            
                FullName = u.FullName,
                email = u.email,
               
            });
        }

        public async Task<ResultUserDTO?> UpdateUserAsync(UpdateUserDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.id);
            if (user == null)
                return null;

            using var hmac = new HMACSHA512();

            user.firstName = dto.firstName;
            user.lastName = dto.lastName;
            user.tckimlik = dto.tckimlik;
            user.dogumTarihi = dto.dogumTarihi;
            user.telNo = dto.telNo;
            user.email = dto.email;
            user.personnelPhoto = dto.personnelPhoto;
            user.PasswordSalt = dto.PasswordSalt;
            user.PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.PasswordSalt)));
            user.departmentId = dto.departmentId;
            user.adres = dto.adres;

            await _userRepository.UpdateAsync(user);

            return new ResultUserDTO
            {
   
                FullName = user.firstName + " " + user.lastName,
                email = user.email
            };
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            var id = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.email == email)?.id;
            if (id == null) return false;
            var user = await _userRepository.GetByIdAsync((int)id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user);
            return true;
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
                FullName = u.FullName,
                email = u.email,
            });
        }
        
        //public async Task<ResultUserDTO> RegisterAsync(CreateUserDTO dto)
        //{
        //    using var hmac = new HMACSHA512();
        //    var user = new User
        //    {
        //        firstName = dto.firstName,
        //        lastName = dto.lastName,
        //        tckimlik = dto.tckimlik,
        //        dogumTarihi = dto.dogumTarihi,
        //        email = dto.email,
        //        PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.PasswordSalt))),
        //        PasswordSalt = Convert.ToBase64String(hmac.Key)
        //    };

        //    await _userRepository.AddAsync(user);

        //    return new ResultUserDTO
        //    {
        //        FullName = user.FullName,
        //        email = user.email,   
        //    };
        //}

        public Task<IEnumerable<ResultUserDTO>> FilterUsersAsync(string department, string position)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

//   Açıklamalar:
//GetAllAsync() → tüm kullanıcıları alıyoruz ve FirstOrDefault ile username’e göre filtreliyoruz.
//Not: Küçük projelerde sorun olmaz, ama büyük veri için repository’ye GetByUsernameAsync gibi bir metot eklemek daha doğru olur.
//Hash ve salt ile parola doğrulama yapılır.
//Eğer kullanıcı bulunamaz veya şifre yanlışsa null dönüyoruz(dilersen özel exception da fırlatabilirsin).
//Şifreler hiçbir zaman DTO veya API response’da dönmez.