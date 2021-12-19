using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.ToDos;

namespace TM.Infrastructure.Data.Repositories
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(TaskManagerContext dbContext) : base(dbContext)
        {
        }

        public Task<Card> GetCardDetails(int cardId, int projectId)
        {
            return Entities.Include(_ => _.Priority)
                           .Include(_ => _.CardHistories)
                           .Include(_ => _.CardTags).ThenInclude(_ => _.Tag)
                           .Include(_ => _.CardAssigns).ThenInclude(_ => _.User)
                           .Include(_ => _.Todos).ThenInclude(_ => _.InverseParent)
                           
                           .FirstOrDefaultAsync(_ => _.Id == cardId && _.ProjectId == projectId);
        }

        public Task<Card> GetCardPhase(int cardId)
        {
            return Entities.Include(_ => _.CardMovements.Where(_ => (bool)_.IsCurrent))
                           .ThenInclude(_ => _.Phase)
                           .FirstOrDefaultAsync(_ => _.Id == cardId);
        }
        public Task<Card> GetCardTodo(int cardId)
        {
            return Entities.Include(_ => _.Todos)
                           .ThenInclude(_ => _.InverseParent)
                           .FirstOrDefaultAsync(_ => _.Id == cardId);
        }

        public async Task<TDataType> GetCardField<TDataType>(int cardId, string columnName)
        {
            return await Entities.Where(_ => _.Id == cardId)
                .Select(_ => (TDataType)_.GetType()
                                        .GetProperty(columnName)
                                        .GetValue(_, null)
                ).FirstOrDefaultAsync();
        }
    }
}
