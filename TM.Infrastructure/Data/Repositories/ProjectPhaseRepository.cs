using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.ProjectPhases;

namespace TM.Infrastructure.Data.Repositories
{
    public class ProjectPhaseRepository : Repository<ProjectPhase>, IProjectPhaseRepository
    {
        public ProjectPhaseRepository(TaskManagerContext dbContext) : base(dbContext)
        {
            
        }
    }
}
