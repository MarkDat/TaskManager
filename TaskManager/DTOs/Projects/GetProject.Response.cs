using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.Phases;
using TM.API.DTOs.ProjectMembers;
using TM.API.DTOs.Users;
using TM.Domain.Entities.Phases;

namespace TM.API.Services.Projects
{
    public class GetProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GetProjectMemberResonse> ProjectMembers { get; set; }
    }
}
