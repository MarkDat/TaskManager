using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.Users;
using TM.API.Services.Projects;

namespace TM.API.DTOs.ProjectMembers
{
    public class GetProjectMemberResonse
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public bool Owner { get; set; }

        //public virtual GetProjectResponse Project { get; set; }
        public  GetUserResponse User { get; set; }
    }
}
