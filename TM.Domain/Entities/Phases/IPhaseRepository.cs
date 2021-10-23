using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.Phases
{
    public interface IPhaseRepository : IRepository<Phase>
    {
        public Task<IEnumerable<Phase>> GetPhasesAndTaskByProjectIdAsync(int projectId);
    }
}
