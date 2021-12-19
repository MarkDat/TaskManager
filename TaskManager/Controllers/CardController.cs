using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TM.API.DTOs.CardAssigns;
using TM.API.DTOs.CardHistories;
using TM.API.DTOs.Cards;
using TM.API.DTOs.CardTags;
using TM.API.DTOs.Tags;
using TM.API.DTOs.Todos;
using TM.API.DTOs.Users;
using TM.API.Services.interfaces;
using TM.Domain.Shared;

namespace TM.API.Controllers
{
    [Authorize]
    [Route("api/card")]
    [ApiController]
    public class CardController : BaseController
    {
        private readonly ICardService _service;
        private readonly ILogger<CardController> _logger;

        public CardController(ILogger<CardController> logger
            , ICardService service)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        ///  Add new card
        /// </summary>
        /// <param name="request">projectId, cardName</param>
        /// <returns>bool</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<AddCardResponse> Add([FromBody] AddCardRequest request)
        {
            var newCard = await _service.Add(request,
                            CreateAddCardHistoryRequest(HistoryActionType.Added));

            return newCard;
        }

        /// <summary>
        ///  Get card details
        /// </summary>
        /// <param name="request">projectId, cardId</param>
        /// <returns>card</returns>
        [HttpPost("details")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<GetCardResponse> Get([FromBody] GetCardRequest request)
        {
            var card = await _service.Get(request);

            return card;
        }

        /// <summary>
        /// Assign card for user
        /// </summary>
        /// <param name="request">cardId, userId</param>
        /// <returns>bool</returns>
        [HttpPost("assign")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<BasicUserResponse> AssignCard([FromBody] AddCardAssignRequest request)
        {
            var user = await _service.AssignCard(request,
                            CreateAddCardHistoryRequest(HistoryActionType.Assign));

            return user;
        }

        /// <summary>
        /// Add tag for card
        /// </summary>
        /// <param name="request">cardId, tagId</param>
        /// <returns>bool</returns>
        [HttpPost("tag")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<GetTagResponse> CardTag([FromBody] CardTagRequest request)
        {
            var tag = await _service.AddTag(request,
                        CreateAddCardHistoryRequest(HistoryActionType.Added));

            return tag;
        }

        /// <summary>
        /// Add todo and sub-todo
        /// </summary>
        /// <param name="request">cardId, parentId?, todoName</param>
        /// <returns>bool</returns>
        [HttpPost("todo")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<AddTodoResponse> AddTodo([FromBody] AddTodoRequest request)
        {
            var todoResponse = await _service.AddTodo(request,
                                CreateAddCardHistoryRequest(HistoryActionType.Added));

            return todoResponse;
        }

        /// <summary>
        /// Get todo
        /// </summary>
        /// <param name="request">cardId</param>
        /// <returns>Todo</returns>
        [HttpGet("{cardId:int}/todo")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IList<TodoModel>> GetTodos([FromRoute] int cardId)
        {
            var todos = await _service.GetTodos(cardId);

            return todos;
        }

        /// <summary>
        /// Get history
        /// </summary>
        /// <param name="request">cardId</param>
        /// <returns>History</returns>
        [HttpGet("{cardId:int}/history")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IList<GetCardHistoryResponse>> GetHistory([FromRoute] int cardId)
        {
            var history = await _service.GetHistory(cardId);

            return history;
        }

        /// <summary>
        ///  Move card to phase and order(developing)
        /// </summary>
        /// <param name="request">cardId, phaseId</param>
        /// <returns>bool</returns>
        [HttpPut("{cardId:int}/phase/{phaseId:int}/move")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<bool> OrderCard([FromRoute] int cardId, [FromRoute] int phaseId)
        {
            var isUpdate = await _service.OrderCard(cardId, phaseId,
                        CreateAddCardHistoryRequest(HistoryActionType.Move));

            return isUpdate;
        }

        /// <summary>
        ///  Update Name, DueDate, Description, Priority by CardId
        /// </summary>
        /// <param name="propertyName">field name</param>
        /// <param name="request">cardId,value</param>
        /// <returns>bool</returns>
        [HttpPut("{propertyName:alpha}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<bool> Update([FromRoute] string propertyName, [FromBody] UpdateCardRequest request)
        {
            var isUpdate = await _service.UpdateProperty(propertyName, request,
                                 CreateAddCardHistoryRequest(HistoryActionType.Updated));

            return isUpdate;
        }

        /// <summary>
        ///  Update todo
        /// </summary>
        /// <param name="request">todo</param>
        /// <returns>bool</returns>
        [HttpPut("todo")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<bool> UpdateTodo([FromBody] TodoUpdateModel request)
        {
            var isUpdate = await _service.UpdateTodo(request,
                                 CreateUpdateHistoryRequest(HistoryActionType.Updated));

            return isUpdate;
        }

    }
}
