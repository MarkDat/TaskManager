using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.Phases;
using TM.API.DTOs.ProjectMembers;
using TM.API.DTOs.Projects;
using TM.API.DTOs.Users;
using TM.API.Services.interfaces;
using TM.API.Services.Phases;
using TM.API.Services.Projects;

namespace TM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _service;
        private readonly IPhaseService _phaseService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger
            , IProjectService service
            , IPhaseService phaseService)
        {
            _service = service;
            _logger = logger;
            _phaseService = phaseService;
        }

        /// <summary>
        /// Get all projects by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List project</returns>
        [HttpGet("user")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<GetProjectResponse>> GetProjectsByCurrentUser()
        {
            var projects = await _service.GetProjectsByCurrentUser(GetUserIdGlobal());

            return projects;
        }

        /// <summary>
        /// Get phase and list card by project id
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>List phase</returns>
        [HttpGet("{id}/phase")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<GetPhaseResponse>> GetPhaseByProjectId([FromRoute] int id)
        {
            var phases = await _phaseService.GetPhaseByProjectId(id);

            return phases;
        }

        /// <summary>
        ///  Add user to project
        /// </summary>
        /// <param name="request">userId, projectId</param>
        /// <returns>List user in project</returns>
        [HttpPost("user")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<GetUserResponse>> AddUserToProject([FromBody] AddProjectMemberRequest request)
        {
            var users = await _service.AddUserToProject(request);

            return users;
        }

        /// <summary>
        ///  Add new a project
        /// </summary>
        /// <param name="request">Name</param>
        /// <returns>New a project which just created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<AddProjectResponse> AddNewProject([FromBody] AddProjectRequest request)
        {
            request.UserId = GetUserIdGlobal();
            var newProject = await _service.AddNewProject(request);

            return newProject;
        }


    }
}
