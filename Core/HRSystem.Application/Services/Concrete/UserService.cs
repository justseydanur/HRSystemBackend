using HRSystem.Application.DTOS.UserDTO;
using HRSystem.Domain.Entities;
using HRSystem.Application.Services.Abstract;
using System.Threading.Tasks;
using HRSystem.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Collections.Generic;

namespace HRSystem.Application.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // Implementation of IUserService.AllUsersAsync
        public Task AllUsersAsync => GetAllUsersAsync();

        public async Task<ResultUserDTO> CreateUserAsync(CreateUserDTO dto)
        {
            using var hmac = new HMACSHA512();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Role = dto.Role,
                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = Convert.ToBase64String(passwordSalt)
            };

            await _userRepository.AddAsync(user);

            return new ResultUserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<ResultUserDTO> LoginUserAsync(LoginUserDTO dto)
        {
            var user = (await _userRepository.GetAllAsync())
                       .FirstOrDefault(u => u.Username == dto.Username);

            if (user == null)
                return null;

            using var hmac = new HMACSHA512(Convert.FromBase64String(user.PasswordSalt));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            var computedHashString = Convert.ToBase64String(computedHash);

            if (computedHashString != user.PasswordHash)
                return null;

            return new ResultUserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("YourSecretKey123!");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IEnumerable<ResultUserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new ResultUserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role
            });
        }
    }
}


//   Açıklamalar:
//GetAllAsync() → tüm kullanıcıları alıyoruz ve FirstOrDefault ile username’e göre filtreliyoruz.
//Not: Küçük projelerde sorun olmaz, ama büyük veri için repository’ye GetByUsernameAsync gibi bir metot eklemek daha doğru olur.
//Hash ve salt ile parola doğrulama yapılır.
//Eğer kullanıcı bulunamaz veya şifre yanlışsa null dönüyoruz(dilersen özel exception da fırlatabilirsin).
//Şifreler hiçbir zaman DTO veya API response’da dönmez.