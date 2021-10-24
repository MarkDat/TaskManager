using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.Tags;

namespace TM.Domain.Entities.CardTags
{
    public partial class CardTag
    {
        public CardTag()
        {

        }
        public CardTag(Tag tag) : base()
        {
            Tag = tag;
        }
        public CardTag(Card card, Tag tag) : base()
        {
            Card = card;
            Tag = tag;
        }

    }
}
