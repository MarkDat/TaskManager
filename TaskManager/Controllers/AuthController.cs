using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
using TM.API.Services.Users;

namespace TM.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserService _service;
        private readonly ILogger<AuthController> _logger;
        public AuthController(
            ILogger<AuthController> logger
            , IUserService service)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        ///    Login
        /// </summary>
        /// <param name="request">username, password</param>
        /// <returns>status user</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<LoginUserResponse> Login([FromBody] LoginUserRequest request)
        {
           var user = await _service.AuthenticateUser(request);

           if (user.IsSuccess)
            {
                return user;
            }

            return user;
        }

    }
}
