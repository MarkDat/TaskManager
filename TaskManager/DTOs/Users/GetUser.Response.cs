using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.ProjectMembers;

namespace TM.API.DTOs.Users
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        // IEnumerable<GetProjectMemberResonse> ProjectMembers { get; set; }
    }
}
