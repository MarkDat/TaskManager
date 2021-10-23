using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.Users;

namespace TM.Domain.Entities.CardAssigns
{
    public partial class CardAssign
    {
        public CardAssign()
        {

        }
        public CardAssign(User user) : base()
        {
            User = user;
        }
        public CardAssign(
            User user
            , Card card) : base()
        {
            User = user;
            Card = card;
        }
    }
}
