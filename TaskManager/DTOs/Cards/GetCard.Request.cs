using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Cards
{
    public class GetCardRequest
    {
        public int ProjectId { get; set; }
        public int CardId { get; set; }
    }
}
