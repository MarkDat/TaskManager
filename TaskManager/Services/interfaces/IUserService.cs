using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.Users;
using TM.Domain.Entities.Users;

namespace TM.API.Services.interfaces
{
    public interface IUserService
    {
        public Task<LoginUserResponse> AuthenticateUser(LoginUserRequest request);
        public JwtSecurityToken GenerateJwtSecurityToken(User user);
    }
}
