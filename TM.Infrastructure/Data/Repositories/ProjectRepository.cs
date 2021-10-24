using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.Projects;

namespace TM.Infrastructure.Data.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskManagerContext dbContext) : base(dbContext)
        {
            
        }

        public Task<Project> GetProjectsAndRelated(int idProject)
        {
            return Entities.Include(_ => _.Cards)
                            .Include(_ => _.ProjectMembers)
                            .Include(_ => _.ProjectPhases)
                            .FirstOrDefaultAsync(_ => _.Id == idProject);
        }

        public async Task<IEnumerable<Project>> GetProjectsByIdUser(int userId)
        {

            var projects =  await Entities.Include(_ => _.ProjectMembers)
                                .ThenInclude(_ => _.User).ToListAsync();

            return projects.Where(_ =>
                           _.ProjectMembers.Any(_ => _.UserId == userId));
        }


    }
}
