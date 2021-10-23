using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.Cards
{
    public interface ICardRepository : IRepository<Card>
    {
        public Task<Card> GetCardPhase(int cardId);
        public Task<Card> GetCardTodo(int cardId);
    }
}
