using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.CardAssigns;
using TM.API.DTOs.CardHistories;
using TM.API.DTOs.Cards;
using TM.API.DTOs.CardTags;
using TM.API.DTOs.Tags;
using TM.API.DTOs.Todos;
using TM.API.DTOs.Users;

namespace TM.API.Services.interfaces
{
    public interface ICardService
    {
        public Task<AddCardResponse> Add(AddCardRequest request, AddCardHistoryRequest history);
        public Task<bool> UpdateProperty(string propertyName
            , UpdateCardRequest request
            , AddCardHistoryRequest history);
        public Task<bool> OrderCard(int cardId, int phaseId, AddCardHistoryRequest history);
        public Task<BasicUserResponse> AssignCard(AddCardAssignRequest request, AddCardHistoryRequest history);
        public Task<GetTagResponse> AddTag(CardTagRequest request, AddCardHistoryRequest history);
        public Task<AddTodoResponse> AddTodo(AddTodoRequest request, AddCardHistoryRequest history);
    }
}
