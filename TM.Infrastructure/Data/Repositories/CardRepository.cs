using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.Cards;

namespace TM.Infrastructure.Data.Repositories
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(TaskManagerContext dbContext) : base(dbContext)
        {
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
    }
}
