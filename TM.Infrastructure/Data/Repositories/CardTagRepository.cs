using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.CardTags;

namespace TM.Infrastructure.Data.Repositories
{
    public class CardTagRepository : Repository<CardTag>, ICardTagRepository
    {
        public CardTagRepository(TaskManagerContext dbContext) : base(dbContext)
        {
        }

        public async Task<CardTag> GetCardTagAsync(int? cardId, int? tagId)
        {
            return await Entities.FirstOrDefaultAsync(_ => _.CardId == cardId
                                                            && _.TagId == tagId);
        }
    }
}
