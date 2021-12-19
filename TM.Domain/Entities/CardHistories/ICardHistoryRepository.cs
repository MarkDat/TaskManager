using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.CardHistories
{
    public interface ICardHistoryRepository : IRepository<CardHistory>
    {
        public Task<IList<CardHistory>> GetHistories(int cardId);
    }
}
