using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Users
{
    public class LoginUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
