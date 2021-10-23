using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.ProjectMembers;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.Users;

namespace TM.UnitTest.UnitTests.Services
{
    public class ProjectServiceSeeder
    {
        protected readonly IEnumerable<Project> Projects =
            new List<Project>()
            {
                new Project
                {
                    Id = 1,
                    Name = "Project1",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    
                }
            };

        protected readonly IEnumerable<User> Users =
            new List<User>()
            {
                new User
                {
                   Id = 1,
                   FirstName = "Dat",
                   LastName = "Luong",
                   IsActive = true
                }
            };
        protected readonly IEnumerable<ProjectMember> ProjectMembers =
            new List<ProjectMember>()
            {
                new ProjectMember
                {
                  IsActive = true,
                  UserId = 1,
                  ProjectId = 1,
                }
            };
    }
}
