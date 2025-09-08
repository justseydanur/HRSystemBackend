using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Services.Abstract
{
    public class ITokenService
    {
        public object CreateToken(int? ıd, string email, object role)
        {
            throw new NotImplementedException();
        }

        string CreateToken(int userId, string email, string role);
    }
}
