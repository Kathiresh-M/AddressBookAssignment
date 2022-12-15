using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IJWTService
    {
        string GenerateSecurityToken(User user);
        int? ValidateJwtToken(string token);
    }
}
