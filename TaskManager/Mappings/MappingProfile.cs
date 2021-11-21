using AutoMapper;
using System.Linq;
using TM.API.DTOs.CardHistories;
using TM.API.DTOs.Cards;
using TM.API.DTOs.Phases;
using TM.API.DTOs.Priority;
using TM.API.DTOs.ProjectMembers;
using TM.API.DTOs.Tags;
using TM.API.DTOs.Todos;
using TM.API.DTOs.Users;
using TM.API.Services.Projects;
using TM.Domain.Entities.CardHistories;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.Phases;
using TM.Domain.Entities.Priorities;
using TM.Domain.Entities.ProjectMembers;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.Tags;
using TM.Domain.Entities.ToDos;
using TM.Domain.Entities.Users;

namespace TM.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreatePhaseMapping();
            CreateUserMapping();
            CreateMemberProjectMapping();
            CreateProjectMapping();
            CreateCardMapping();
            CreateTodoMapping();
            CreateCardHistoryMapping();
            CreateTagMapping();
            CreatePriorityMapping();
        }
        
        public void CreatePhaseMapping()
        {
            CreateMap<Phase, GetPhaseResponse>()
                    .ForMember(d => d.Cards, 
                    otp => otp.MapFrom(s => s.CardMovements.Where(_=> (bool)_.IsCurrent)
                                                           .Select(x=>x.Card))
                    );
        }

        public void CreateCardHistoryMapping()
        {
            CreateMap<CardHistory, GetCardHistoryResponse>();
        }

        public void CreateProjectMapping()
        {
            CreateMap<Project, GetProjectResponse>().ReverseMap();
        }
        public void CreateUserMapping()
        {
            CreateMap<User, GetUserResponse>();
            CreateMap<User, LoginUserResponse>();
            CreateMap<User, BasicUserResponse>();
        }
        public void CreateMemberProjectMapping()
        {
            CreateMap<ProjectMember, GetProjectMemberResonse>();
        }

        public void CreateCardMapping()
        {
            CreateMap<Card, AddCardResponse>();
            CreateMap<Card, GetCardResponse>()
                .ForMember(d => d.Tags,
                    otp => otp.MapFrom(s => s.CardTags.Select(_ => _.Tag))
                )
                .ForMember(d => d.AssignUser,
                    otp => otp.MapFrom(s => s.CardAssigns.Where(_ => (bool)_.IsAssigned)
                                                        .Select(_ => _.User).FirstOrDefault())
                );
        }

        public void CreateTagMapping()
        {
            CreateMap<Tag, GetTagResponse>();
        }

        public void CreateTodoMapping()
        {
            CreateMap<Todo, GetTodoResponse>().ForMember(d => d.ChildTodos,
                    otp => otp.MapFrom(s => s.InverseParent)
                );
            CreateMap<Todo, AddTodoResponse>();
    }

        public void CreatePriorityMapping()
        {
            CreateMap<Priority, GetPriorityResponse>();
        }
    }
}
