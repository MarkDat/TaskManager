using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.ProjectMembers
{
    public class AddProjectMemberRequest
    {
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
    }
}
