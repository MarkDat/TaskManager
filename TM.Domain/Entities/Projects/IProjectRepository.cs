using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.Projects
{
    public interface IProjectRepository : IRepository<Project>
    {
        public Task<IEnumerable<Project>> GetProjectsByIdUser(int idUser);
        public Task<Project> GetProjectsAndRelated(int idProject);
    }
}
