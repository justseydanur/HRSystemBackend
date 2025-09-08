using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Services.Abstract
{
    public interface ITokenServices
    {
        string CreateToken(int userId, string email, string role);
    }
}
