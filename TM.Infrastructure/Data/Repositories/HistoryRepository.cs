using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.CardHistories;

namespace TM.Infrastructure.Data.Repositories
{
    public class HistoryRepository : Repository<CardHistory>, ICardHistoryRepository
    {
        public HistoryRepository(TaskManagerContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<CardHistory>> GetHistories(int cardId) {
            return await Entities.Where(_ => _.CardId == cardId).ToListAsync();
        }
    }
}
