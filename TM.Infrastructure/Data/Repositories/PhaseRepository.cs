using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.Phases;

namespace TM.Infrastructure.Data.Repositories
{
    public class PhaseRepository : Repository<Phase>, IPhaseRepository
    {
        public PhaseRepository(TaskManagerContext dbContext) : base(dbContext)
        {
        }


        public async Task<IEnumerable<Phase>> GetPhasesAndTaskByProjectIdAsync(int projectId)
        {
            return await Entities.Include(_ => _.CardMovements.Where(_ => _.Card.ProjectId == projectId))
                                            .ThenInclude(_ => _.Card.Priority)
                                            .Include(_ => _.CardMovements.Where(_ => _.Card.ProjectId == projectId))
                                            .ThenInclude(_ => _.Card.Todos).ThenInclude(_ => _.InverseParent)
                                            .Include(_ => _.CardMovements.Where(_ => _.Card.ProjectId == projectId))
                                            .ThenInclude(_ => _.Card.CardHistories)
                                            .Include(_ => _.CardMovements.Where(_ => _.Card.ProjectId == projectId))
                                            .ThenInclude(_ => _.Card.CardTags).ThenInclude(_ => _.Tag)
                                            .Include(_ => _.CardMovements.Where(_ => _.Card.ProjectId == projectId))
                                            .ThenInclude(_ => _.Card.CardAssigns).ThenInclude(_ => _.User)
                                            .Where(_ => _.ProjectPhases.Any(_ => _.ProjectId == projectId))
                                            .ToListAsync();
        }
    }
}
