﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.Phases;
using TM.API.DTOs.ProjectMembers;
using TM.API.DTOs.Projects;
using TM.API.DTOs.Users;
using TM.API.Services.interfaces;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.Phases;
using TM.Domain.Entities.ProjectMembers;
using TM.Domain.Entities.ProjectPhases;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.Users;
using TM.Domain.Interfaces;
using TM.Domain.Resources;
using TM.Domain.Shared;
using TM.Domain.Utilities;

namespace TM.API.Services.Projects
{
    public class ProjectService : BaseService, IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IMemberProjectRepository _memberProjectRepository;
        private readonly IUserRepository _userRepository;
        public ProjectService(IUnitOfWork unitOfWork
           , IMapper mapper
           , IProjectRepository projectRepository
           , IMemberProjectRepository memberProjectRepository
           , IUserRepository userRepository
        ) : base(unitOfWork)
        {
            _projectRepository = projectRepository;
            _memberProjectRepository = memberProjectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProjectResponse>> GetProjectsByCurrentUser(int userId)
        {
            var projects = await _projectRepository.GetProjectsByIdUser(userId);

            var outputProjects = _mapper.Map<List<GetProjectResponse>>(projects);

            return outputProjects;
        }

        public async Task<AddProjectResponse> AddNewProject(AddProjectRequest request)
        {
            return await ExecuteTransaction(async () => {

                var newProject = new Project(request.Name);
                var user = await Repository<User>().FindAsync(request.UserId);
                if (user == null)
                    throw new HttpException(string.Format(Messages.RecordNotFound, "user"));

                await Repository<Project>().InsertAsync(newProject, true);

                newProject.AddProjectMember(user);
                newProject.AddBasicPhases();

                return new AddProjectResponse()
                {
                    Id = newProject.Id,
                    Name = newProject.Name
                };
            });
        }

        public async Task<GetProjectResponse> GetOne(int projectId,int userId)
        {
            return await ExecuteTransaction(async () => {

                var project = await Repository<Project>().FindAsync(projectId);

                return _mapper.Map<GetProjectResponse>(project);
            });
        }

        public async Task<IEnumerable<GetUserResponse>> AddUserToProject(AddProjectMemberRequest request)
        {
            return await ExecuteTransaction(async () => {

                IEnumerable<GetUserResponse> userResponses = null;

                var userRepos = Repository<User>();
                 var projectMemberRepos = Repository<ProjectMember>();

                var project = await Repository<Project>().FindAsync(request.ProjectId);
                var user = await userRepos.FindAsync(request.UserId);

                if (user == null || project == null)
                    throw new KeyNotFoundException();

                var checkIn = _memberProjectRepository.IsHaveProjectMember(request.ProjectId, request.UserId);

                if (!checkIn)
                    project.AddProjectMember(user);
                else
                {
                    return new List<GetUserResponse>();
                }

                var usersInProject = await _userRepository.UsersInProjectAsync(project.Id);
                userResponses = _mapper.Map<IEnumerable<GetUserResponse>>(usersInProject);

                return userResponses;
            });
        }



    }
}
