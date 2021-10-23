using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.Phases;

namespace TM.Domain.Entities.CardMovements
{
    public partial class CardMovement 
    {
        public CardMovement(int? phaseId) : base()
        {
            PhaseId = phaseId;
        }

        public CardMovement(Phase phase) : base()
        {
            Phase = phase;
        }

        public CardMovement(Phase phase, Card card) : base()
        {
            Phase = phase;
            Card = card;
        }
    }
}
