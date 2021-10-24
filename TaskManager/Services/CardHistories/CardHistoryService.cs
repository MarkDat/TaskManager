using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.CardHistories;
using TM.Domain.Entities.CardHistories;
using TM.Domain.Interfaces;

namespace TM.API.Services.CardHistories
{
    public class CardHistoryService : BaseService
    {
        private readonly IMapper _mapper;
        public CardHistoryService(
            IUnitOfWork unitOfWork
            , IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<bool> AddCardHistory(AddCardHistoryRequest request)
        {
            try
            {
                await UnitOfWork.BeginTransaction();

                var cardHistory = UnitOfWork.Repository<CardHistory>();

                await cardHistory.InsertAsync(new CardHistory() { 
                    UserId = request.UserId,
                    CardId = request.CardId,
                    ActionType = request.ActionType.ToString(),
                    Content = $"{request.UserName} {request.ActionType.ToLower()} {request.Content}" 
                });

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }


            return true;
        }
    }
}
