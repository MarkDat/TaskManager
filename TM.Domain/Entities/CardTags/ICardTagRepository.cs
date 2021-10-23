using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.CardTags
{
    public interface ICardTagRepository : IRepository<CardTag>
    {
        Task<CardTag> GetCardTagAsync(int? cardId, int? tagId);
    }
}
