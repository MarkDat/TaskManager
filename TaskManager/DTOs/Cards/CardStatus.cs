using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.Domain.Shared;

namespace TM.API.DTOs.Cards
{
    public class CardStatus
    {
        public bool IsSuccess { get; set; }
        public AcionCard Action { get; set; }
    }
}
