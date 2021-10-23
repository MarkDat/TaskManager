using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.ProjectMembers;

namespace TM.Infrastructure.Data.Repositories
{
    public class ProjectMemberRepository : Repository<ProjectMember>, IMemberProjectRepository
    {
        public ProjectMemberRepository(TaskManagerContext dbContext) : base(dbContext)
        {
        }

        public bool IsHaveProjectMember(int? projectId, int? userId)
        {
            return Entities.Any(_ => _.UserId == userId
                                        && _.ProjectId == projectId
                                        && _.IsActive == true);
        }
    }
}
