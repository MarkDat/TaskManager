using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TM.API.DTOs.CardAssigns;
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
    [Route("api/[controller]")]
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
        ///  Move card to phase and order(developing)
        /// </summary>
        /// <param name="request">cardId, phaseId</param>
        /// <returns>bool</returns>
        [HttpPut("order")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<bool> OrderCard([FromBody] UpdateCardRequest request)
        {
            var isUpdate = await _service.OrderCard(request,
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
    }
}
