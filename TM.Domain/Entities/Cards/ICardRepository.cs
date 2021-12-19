using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Entities.ToDos;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.Cards
{
    public interface ICardRepository : IRepository<Card>
    {
        public Task<Card> GetCardPhase(int cardId);
        public Task<Card> GetCardTodo(int cardId);
        public Task<Card> GetCardDetails(int cardId, int projectId);
    }
}
