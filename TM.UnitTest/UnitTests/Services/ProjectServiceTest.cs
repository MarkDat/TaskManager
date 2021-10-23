using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.API.Services.Projects;
using TM.Infrastructure.Data;
using Xunit;

namespace TM.UnitTest.UnitTests.Services
{
    public class ProjectServiceTest : IClassFixture<ProjectServiceFixture>
    {
        private ProjectServiceFixture _serviceFixture;
        public TaskManagerContext Context =>
            _serviceFixture.Context;
        internal ProjectService Service =>
           _serviceFixture.ProjectService;

        public ProjectServiceTest(ProjectServiceFixture fixture)
        {
            _serviceFixture = fixture;
        }

        [Fact]
        public async Task Get_AllProject_ActiveStatusIsInProgress()
        {
            int userID = 1;
            //Act
            var response = await Service.GetAllByUserId(userID);

            //Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Get_One_ActiveStatusIsInProgress()
        {
            int projectId = 1;
            //Act
            var response = await Service.GetOne(projectId);

            //Assert
            Assert.NotNull(response);
        }
    }
}
