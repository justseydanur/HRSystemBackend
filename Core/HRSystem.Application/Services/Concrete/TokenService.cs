using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HRSystem.Application.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using HRSystem.Application.DTOS.UserDTO;
using HRSystem.Application.DTOS.TokenDTO;

namespace HRSystem.Application.Services.Concrete
{
    public class TokenService : ITokenServices
    {

        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _config;
        public TokenService(IGenericRepository<User> userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }


        public async Task<TokenResponseDTO> CreateTokenResponse(User? user)
        {
            return new TokenResponseDTO
            {
                accessToken = CreateToken(user),
                refreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }


        public async Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
                return null;

            return await CreateTokenResponse(user);
        }

        private async Task<User?> ValidateRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);

        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.SaveAsync();
            return refreshToken;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.firstName + user.lastName),
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config.GetValue<string>("Jwt:Issuer"),
                audience: _config.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }






























        //public string CreateToken(int userId, string email, string role)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        //        new Claim(ClaimTypes.Email, email),
        //        new Claim(ClaimTypes.Role, role)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: _config["Jwt:Issuer"],
        //        audience: _config["Jwt:Audience"],
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpireMinutes"]!)),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
