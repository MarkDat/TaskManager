using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.CardAssigns
{
    public interface ICardAssignRepository : IRepository<CardAssign>
    {
        Task<CardAssign> GetCardAssignIsAssignedAsync(int? cardId);
    }
}
