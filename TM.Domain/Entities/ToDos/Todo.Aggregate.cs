using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.Cards;

namespace TM.Domain.Entities.ToDos
{
    public partial class Todo
    {
        public Todo(string name)
        {
            Name = name;
            IsCheck = false;
        }
        public Todo(string name, bool isCheck)
        {
            Name = name;
            IsCheck = isCheck;
        }

        public Todo(
            Card card
            , string name) : base()
        {
            Card = card;
            Name = name;
        }

    }
}
