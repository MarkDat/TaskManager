using TM.Domain.Interfaces;

namespace TM.Domain.Entities.ProjectMembers
{
    public interface IMemberProjectRepository : IRepository<ProjectMember>
    {
        bool IsHaveProjectMember(int? projectId, int? userId);
    }
}
