using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.Phases;

namespace TM.API.Services.interfaces
{
    public interface IPhaseService
    {
        public Task<IEnumerable<GetPhaseResponse>> GetPhaseByProjectId(int projectId);
    }
}
