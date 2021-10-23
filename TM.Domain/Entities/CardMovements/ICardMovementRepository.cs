using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.CardMovements
{
    public interface ICardMovementRepository : IRepository<CardMovement>
    {
        Task<CardMovement> GetCurrentPhaseByCardIdAsync(int? cardId);
    }
}
