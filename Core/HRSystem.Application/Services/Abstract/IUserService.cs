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
        Task AllUsersAsync { get; }

        Task<ResultUserDTO> CreateUserAsync(CreateUserDTO dto);

        Task<IEnumerable<ResultUserDTO>> GetAllUsersAsync();
        Task<ResultUserDTO> LoginUserAsync(LoginUserDTO dto);
    }
}
