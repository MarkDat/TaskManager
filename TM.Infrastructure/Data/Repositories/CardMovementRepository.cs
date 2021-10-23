using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.CardMovements;

namespace TM.Infrastructure.Data.Repositories
{
    public class CardMovementRepository : Repository<CardMovement>, ICardMovementRepository
    {
        public CardMovementRepository(TaskManagerContext dbContext) : base(dbContext)
        {
        }

        public async Task<CardMovement> GetCurrentPhaseByCardIdAsync(int? cardId)
        {
            return await Entities
                            .Include(_ => _.Phase)
                            .Include(_ => _.Card)
                            .FirstOrDefaultAsync(_ => _.CardId == cardId
                                                        && (bool)_.IsCurrent);
        }

    }
}
