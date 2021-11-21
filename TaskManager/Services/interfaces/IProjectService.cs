using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.ProjectMembers;
using TM.API.DTOs.Projects;
using TM.API.DTOs.Users;
using TM.API.Services.Projects;

namespace TM.API.Services.interfaces
{
    public interface IProjectService
    {
        public Task<IEnumerable<GetProjectResponse>> GetProjectsByCurrentUser(int userId);
        public Task<AddProjectResponse> AddNewProject(AddProjectRequest request);
        public Task<IEnumerable<GetUserResponse>> AddUserToProject(AddProjectMemberRequest request);
        public Task<GetProjectResponse> GetOne(int projectId, int userId);


    }
}
