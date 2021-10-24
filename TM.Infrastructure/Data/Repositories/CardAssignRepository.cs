using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.CardAssigns;

namespace TM.Infrastructure.Data.Repositories
{
    public class CardAssignRepository : Repository<CardAssign>, ICardAssignRepository
    {
        public CardAssignRepository(TaskManagerContext dbContext) : base(dbContext)
        {
        }

        public async Task<CardAssign> GetCardAssignIsAssignedAsync(int? cardId)
        {
           return await Entities
                            .FirstOrDefaultAsync(_ => _.CardId == cardId
                                                        && (bool)_.IsAssigned);
        }
    }
}
