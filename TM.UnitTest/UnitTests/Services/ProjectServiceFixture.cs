using Microsoft.EntityFrameworkCore;
using System;
using TM.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TM.API.Services.Projects;
using TM.Domain.Interfaces;
using AutoMapper;
using TM.Domain.Entities.Projects;

namespace TM.UnitTest.UnitTests.Services
{
    public class ProjectServiceFixture : ProjectServiceSeeder, IDisposable
    {
        private DbContextOptions<TaskManagerContext> _options =
            new DbContextOptionsBuilder<TaskManagerContext>()
                .UseInMemoryDatabase("ProjectServiceTest")
                .Options;

        public TaskManagerContext Context { get; }
        public ProjectService ProjectService { get; private set; }
        public Mock<IProjectRepository> _mockProjectRepository { get; private set; }
        public Mock<IUnitOfWork> _mockUnitOfWork { get; private set; }
        public Mock<IMapper> _mockMapper { get; private set; }
        public Mock<ILogger<ProjectService>> _loggerMock;

        public IProjectRepository ProjectRepository { get; private set; }

        public ProjectServiceFixture()
        {
            Context = new TaskManagerContext(_options);
            Seed();
            _loggerMock = new Mock<ILogger<ProjectService>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockProjectRepository = new Mock<IProjectRepository>();

            
            ProjectService = new ProjectService(_mockUnitOfWork.Object
                , _mockMapper.Object
                , _mockProjectRepository.Object);
        }

        public void Dispose()
        {
            Context.Dispose();
            ProjectService = null;
            _options = null;
            _loggerMock = null;
            _mockUnitOfWork = null;
            _mockMapper = null;
            _mockProjectRepository = null;
        }

        private void Seed()
        {
            Context.Projects.AddRange(Projects);
            Context.Users.AddRange(Users);
            Context.ProjectMembers.AddRange(ProjectMembers);
            Context.SaveChangesAsync();
        }
    }
}
