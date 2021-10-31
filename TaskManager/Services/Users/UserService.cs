using AutoMapper;
using GMPMS.Entities.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TM.API.DTOs.Users;
using TM.API.Services.interfaces;
using TM.Domain.Entities.Users;
using TM.Domain.Interfaces;
using TM.Domain.Utilities;

namespace TM.API.Services.Users
{
    public class UserService : BaseService, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        public UserService(
            IUnitOfWork unitOfWork
            , IMapper mapper
            , IConfiguration config
            , IUserRepository userRepository) : base(unitOfWork)
        {
            _mapper = mapper;
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<LoginUserResponse> AuthenticateUser(LoginUserRequest request)
        {
            var user = await _userRepository.Login(request.UserName, request.Password);
            if (user == null)
                throw new HttpException(Messages.IncorrectCredential, System.Net.HttpStatusCode.Unauthorized);

            var token = GenerateJwtSecurityToken(user);
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginUserResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public JwtSecurityToken GenerateJwtSecurityToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Name", $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

            
            return token;
        }
    }
}
