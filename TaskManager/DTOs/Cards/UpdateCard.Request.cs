using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Cards
{
    public class UpdateCardRequest
    {
        public int? CardId { get; set; }
        public int? PhaseId { get; set; }
        public int order { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
