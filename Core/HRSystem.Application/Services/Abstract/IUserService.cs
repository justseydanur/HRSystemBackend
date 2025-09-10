using HRSystem.Application.DTOS.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Services.Abstract
{
    public interface IUserService
    {
        Task<ResultUserDTO> CreateUserAsync(CreateUserDTO dto);
        Task<ResultUserDTO?> UpdateUserAsync(UpdateUserDTO dto);
        Task<DetailUserDTO?> GetUserByIdAsync(int id);
        Task<IEnumerable<ResultUserDTO>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
        Task<ResultUserDTO?> LoginUserAsync(LoginUserDTO dto);
        //Task<ResultUserDTO> RegisterAsync(CreateUserDTO dto);

        // Yeni eklenen metodlar
        Task<IEnumerable<ResultUserDTO>> FilterUsersAsync(string department, string position);
        Task<IEnumerable<ResultUserDTO>> SearchUsersAsync(string query);
        Task<bool> DeleteUserAsync(string email);
    }
}
