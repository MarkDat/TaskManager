using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
using TM.API.Services.CardHistories;
using TM.API.Services.interfaces;
using TM.Domain.Entities.CardAssigns;
using TM.Domain.Entities.CardHistories;
using TM.Domain.Entities.CardMovements;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.CardTags;
using TM.Domain.Entities.Phases;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.Tags;
using TM.Domain.Entities.ToDos;
using TM.Domain.Entities.Users;
using TM.Domain.Interfaces;
using TM.Domain.Resources;
using TM.Domain.Shared;
using TM.Domain.Utilities;

namespace TM.API.Services.Cards
{
    public class CardService : BaseService, ICardService
    {
        private readonly IMapper _mapper;
        private readonly ICardAssignRepository _cardAssignRepository;
        private readonly ICardTagRepository _cardTagRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IToDoRepository _todoRepository;
        public CardService(
            IUnitOfWork unitOfWork
            , IMapper mapper
            , ICardAssignRepository cardAssignRepository
            , ICardTagRepository cardTagRepository
            , ICardRepository cardRepository
            , IToDoRepository todoRepository) : base(unitOfWork)
        {
            _mapper = mapper;
            _cardAssignRepository = cardAssignRepository;
            _cardTagRepository = cardTagRepository;
            _cardRepository = cardRepository;
            _todoRepository = todoRepository;
        }

        public async Task<GetCardResponse> Get(GetCardRequest request)
        {
            return await ExecuteTransaction(async () =>
            {

                var project = await Repository<Project>().FindAsync(request.ProjectId);
                if (project is null)
                    throw new HttpException(string.Format(Messages.RecordNotFound, "project"));

                var card = await _cardRepository.GetCardDetails(request.CardId, request.ProjectId);
                if (card is null)
                    throw new HttpException(string.Format(Messages.RecordNotFound, "card"));

                return _mapper.Map<GetCardResponse>(card);
            });
        }

        public async Task<AddCardResponse> Add(AddCardRequest request, AddCardHistoryRequest history)
        {
            return await ExecuteTransaction(async () =>
            {
                var newCard = new Card(request.Name);

                var project = await Repository<Project>().FindAsync(request.ProjectId);
                if (project == null)
                    throw new HttpException(string.Format(Messages.RecordNotFound, "project"));

                newCard.AddCardToProject(project);
                newCard.DefaultPhaseForCard();

                string writeLog = "this card";
                newCard.AddHistory(ConvertToCardHistory(history, writeLog));

                await UnitOfWork.Repository<Card>().InsertAsync(newCard);

                return _mapper.Map<AddCardResponse>(newCard);
            });
        }
        public async Task<bool> UpdateTodo(TodoUpdateModel request, AddCardHistoryRequest history)
        {
            return await ExecuteTransaction(async () =>
            {

                var todo = await Repository<Todo>().FindAsync(request.Id);
                if (todo is null)
                    throw new KeyNotFoundException();

                _mapper.Map(request, todo);

                return true;
            });
        }

        public async Task<bool> UpdateProperty(
            string propertyName
            , UpdateCardRequest request
            , AddCardHistoryRequest history)
        {
            return await ExecuteTransaction(async () =>
            {

                var card = await Repository<Card>().FindAsync(request.CardId);
                if (card == null)
                    throw new KeyNotFoundException();

                switch (propertyName)
                {
                    case "name":
                        card.Name = request.Value ?? card.Name;
                        break;
                    case "duedate":
                        DateTime? datetime = string.IsNullOrEmpty(request.Value)
                                            ? null : DateTime.Parse(request.Value);
                        card.DueDate = datetime ?? card.DueDate;
                        break;
                    case "description":
                        card.Description = request.Value ?? card.Description;
                        break;
                    case "priority":
                        if (request.Value == null) break;
                        card.PriorityId = int.Parse(request.Value);
                        break;
                    default:
                        throw new HttpException(Messages.InvalidVariableName);
                }

                string writeLog = propertyName;
                card.AddHistory(ConvertToCardHistory(history, writeLog));

                return true;
            });
        }

        public async Task<bool> OrderCard(int cardIdCurrent, int phaseIdMove, AddCardHistoryRequest history)
        {
            return await ExecuteTransaction(async () =>
            {

                var movePhase = await UnitOfWork.Repository<Phase>().FindAsync(phaseIdMove);
                var card = await _cardRepository.GetCardPhase((int)cardIdCurrent);
                var cardMovement = card.CardMovements?.FirstOrDefault();

                if (cardMovement == null || movePhase == null)
                    throw new HttpException(string.Format(Messages.RecordNotFound, "card movement and move phase"));

                if (cardMovement.PhaseId == (int)PhaseBasic.Destroy)
                    throw new HttpException("You can't move");

                //get id and name to support write log history
                var cardId = cardMovement.Card.Id;
                string currentPhaseName = cardMovement.Phase.Name,
                       movePhaseName = movePhase.Name;

                if (movePhase.Id == (int)PhaseBasic.Destroy
                   || movePhase.AcceptMoveId == cardMovement.Phase.Id)
                {
                    cardMovement.IsCurrent = false;
                    card.AddNewMovement(movePhase);
                }
                else
                    ErrorMovePhase(movePhase.AcceptMoveId);

                string writeLog = $"card from phase {currentPhaseName} to phase {movePhaseName}";
                card.AddHistory(ConvertToCardHistory(history, writeLog));

                return true;
            });
        }

        private void ErrorMovePhase(int? acceptMoveId)
        {
            switch (acceptMoveId)
            {
                case (int)PhaseBasic.Destroy:
                    throw new HttpException(string.Format(Messages.MoveCardException, PhaseBasic.Destroy.ToValue()));

                case (int)PhaseBasic.Completed:
                    throw new HttpException(string.Format(Messages.MoveCardException, PhaseBasic.Completed.ToValue()));

                case (int)PhaseBasic.Opportunity:
                    throw new HttpException(string.Format(Messages.MoveCardException, PhaseBasic.Opportunity.ToValue()));

                case (int)PhaseBasic.Order:
                    throw new HttpException(string.Format(Messages.MoveCardException, PhaseBasic.Order.ToValue()));

                case (int)PhaseBasic.Quote:
                    throw new HttpException(string.Format(Messages.MoveCardException, PhaseBasic.Quote.ToValue()));
                default:
                    throw new HttpException("You can't move !");
            }
        }

        public async Task<BasicUserResponse> AssignCard(AddCardAssignRequest request, AddCardHistoryRequest history)
        {
            BasicUserResponse basicUser = null;
            try
            {
                await UnitOfWork.BeginTransaction();

                var cardAssignRepo = UnitOfWork.Repository<CardAssign>();

                var user = await UnitOfWork.Repository<User>().FindAsync(request.UserId);
                var card = await UnitOfWork.Repository<Card>().FindAsync(request.CardId);

                //1. Check user and card null
                if (card == null || user == null)
                    throw new KeyNotFoundException();

                //2. Check assigned card 
                var cardAssign = await _cardAssignRepository.GetCardAssignIsAssignedAsync(request.CardId);
                if (cardAssign != null) cardAssign.IsAssigned = false;

                card.AddAssign(user);

                var writeLog = $"card for {user.FirstName} {user.LastName}";
                card.AddHistory(ConvertToCardHistory(history, writeLog));

                basicUser = _mapper.Map<BasicUserResponse>(user);
                await UnitOfWork.CommitTransaction();

            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }


            return basicUser;
        }

        public async Task<GetTagResponse> AddTag(CardTagRequest request, AddCardHistoryRequest history)
        {
            GetTagResponse tagResponse = null;
            try
            {
                await UnitOfWork.BeginTransaction();

                var card = await UnitOfWork.Repository<Card>().FindAsync(request.CardId);
                var tag = await UnitOfWork.Repository<Tag>().FindAsync(request.TagId);

                //1. Check user and card null
                if (card == null || tag == null)
                    throw new KeyNotFoundException();

                //2. Check assigned card 
                var cardTag = await _cardTagRepository.GetCardTagAsync(request.CardId, request.TagId);
                if (cardTag == null)
                    card.AddCardTag(tag);
                else
                {
                    await UnitOfWork.RollbackTransaction();
                    return tagResponse;
                }

                string writeLog = $"tag named {tag.Name}";
                card.AddHistory(ConvertToCardHistory(history, writeLog));

                tagResponse = _mapper.Map<GetTagResponse>(tag);
                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }


            return tagResponse;
        }

        #region Todo
        public async Task<IList<TodoModel>> GetTodos(int cardId)
        {
            return await ExecuteTransaction(async () =>
            {
                var todos = await _todoRepository.GetTodos(cardId);

                return _mapper.Map<IList<TodoModel>>(todos);
            });
        }

        public async Task<AddTodoResponse> AddTodo(AddTodoRequest request, AddCardHistoryRequest history)
        {
            return await ExecuteTransaction(async () =>
            {
                AddTodoResponse todoResponse = null;

                var writeLog = "";
                Todo temp = null;

                var card = await _cardRepository.GetCardTodo((int)request.CardId);
                if (card is null)
                    throw new KeyNotFoundException();

                var todoParent = card.Todos.FirstOrDefault(_ => _.Id == request.ParentId);
                if (todoParent is null)
                {
                    temp = card.AddTodo(request.Name, null);
                    writeLog += $"a todo named {request.Name}";
                }
                else
                {
                    temp = card.AddTodo(request.Name, request.ParentId);
                    writeLog += $"a sub-todo named {request.Name} for {todoParent.Name}";
                }

                card.AddHistory(ConvertToCardHistory(history, writeLog));

                await Repository<Todo>().SaveChangesAsync();

                todoResponse = _mapper.Map<AddTodoResponse>(temp);

                return todoResponse;
            });
        }

        #endregion


        private CardHistory ConvertToCardHistory(AddCardHistoryRequest history, string content)
        {
            return new CardHistory()
            {
                UserId = history.UserId,
                ActionType = history.ActionType.ToString(),
                Content = $"{history.UserName} {history.ActionType.ToLower()} {content}"
            };
        }


    }
}
