using AutoMapper;
using GMPMS.Entities.Resources;
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

        public CardService(
            IUnitOfWork unitOfWork
            , IMapper mapper
            , ICardAssignRepository cardAssignRepository
            , ICardTagRepository cardTagRepository
            , ICardRepository cardRepository) : base(unitOfWork)
        {
            _mapper = mapper;
            _cardAssignRepository = cardAssignRepository;
            _cardTagRepository = cardTagRepository;
            _cardRepository = cardRepository;
        }

        public async Task<AddCardResponse> Add(AddCardRequest request, AddCardHistoryRequest history)
        {
            return await ExecuteTransaction(async () =>
            {
                var newCard = new Card(request.Name);

                var project = await UnitOfWork.Repository<Project>().FindAsync(request.ProjectId);
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

        public async Task<bool> UpdateProperty(
            string propertyName
            , UpdateCardRequest request
            , AddCardHistoryRequest history)
        {
            try
            {
                await UnitOfWork.BeginTransaction();

                var card = await UnitOfWork.Repository<Card>().FindAsync(request.CardId);
                if (card == null)
                    throw new KeyNotFoundException();

                switch (propertyName)
                {
                    case "name":
                        card.Name = (string)request.Value ?? card.Name;
                        break;
                    case "duedate":
                        DateTime? datetime = string.IsNullOrEmpty((string)request.Value)
                                            ? (DateTime?)null : DateTime.Parse((string)request.Value);
                        card.DueDate = datetime ?? card.DueDate;
                        break;
                    case "description":
                        card.Description = (string)request.Value ?? card.Description;
                        break;
                    case "priority":
                        if (request.Value == null) break;
                        card.PriorityId = (int?)(long)request.Value;
                        break;
                    default:
                        await UnitOfWork.RollbackTransaction();
                        return false;
                }

                string writeLog = propertyName;
                card.AddHistory(ConvertToCardHistory(history, writeLog));
                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }

            return true;
        }

        public async Task<bool> OrderCard(int cardIdCurrent, int phaseIdMove, AddCardHistoryRequest history)
        {
            return await ExecuteTransaction(async ()=> {

                var movePhase = await UnitOfWork.Repository<Phase>().FindAsync(phaseIdMove);
                var card = await _cardRepository.GetCardPhase((int)cardIdCurrent);
                var cardMovement = card.CardMovements?.FirstOrDefault();

                if (cardMovement == null || movePhase == null)
                    throw new HttpException(string.Format(Messages.RecordNotFound, "card movement and move phase"));

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
                    throw new HttpException("Can not move !");

                string writeLog = $"card from phase {currentPhaseName} to phase {movePhaseName}";
                card.AddHistory(ConvertToCardHistory(history, writeLog));

                return true;
            });
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

        public async Task<AddTodoResponse> AddTodo(AddTodoRequest request, AddCardHistoryRequest history)
        {
            AddTodoResponse todoResponse = null;
            try
            {
                string writeLog = "";
                Todo temp = null;

                await UnitOfWork.BeginTransaction();

                var card = await _cardRepository.GetCardTodo((int)request.CardId);
                var todoParent = card.Todos.FirstOrDefault(_ => _.Id == request.ParentId);

                if (card == null)
                    throw new KeyNotFoundException();

                if (todoParent == null)
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

                await UnitOfWork.CommitTransaction();

                todoResponse = _mapper.Map<AddTodoResponse>(temp);
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }


            return todoResponse;
        }

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
