using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.ProjectPhases;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.Users;
using TM.Domain.Shared;

namespace TM.Domain.Entities.ProjectMembers
{
    public partial class ProjectMember
    {
        public ProjectMember()
        {

        }
        public ProjectMember(
              int? userId
             , int? projectId) : base()
        {
            UserId = userId;
            ProjectId = projectId;
          
        }

        public ProjectMember(User user,Project project)
        {
            User = user;
            Project = project;
        }

    }
}
