using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.Cards;
using TM.Domain.Entities.Cards;

namespace TM.API.DTOs.Phases
{
    public class GetPhaseResponse {

        public int Id { get; set; }
        public string Name { get; set; }
        public int? AcceptMoveId { get; set; }
        public IEnumerable<GetCardResponse> Cards { get; set; }
    }
}
