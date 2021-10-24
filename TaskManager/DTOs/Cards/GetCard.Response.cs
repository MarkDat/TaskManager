using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.CardHistories;
using TM.API.DTOs.Priority;
using TM.API.DTOs.Tags;
using TM.API.DTOs.Todos;
using TM.API.DTOs.Users;
using TM.Domain.Entities.CardHistories;
using TM.Domain.Entities.Priorities;
using TM.Domain.Entities.Tags;
using TM.Domain.Entities.ToDos;
using TM.Domain.Entities.Users;

namespace TM.API.DTOs.Cards
{
    public class GetCardResponse
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public virtual IEnumerable<GetTodoResponse> Todos { get; set; }
        public virtual GetPriorityResponse Priority { get; set; }
        public virtual IEnumerable<GetTagResponse> Tags { get; set; }
        public BasicUserResponse AssignUser { get; set; }
        public virtual IEnumerable<GetCardHistoryResponse> CardHistories { get; set; }

    }
}
