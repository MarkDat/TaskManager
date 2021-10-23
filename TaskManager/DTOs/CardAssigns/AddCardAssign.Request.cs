using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.CardAssigns
{
    public class AddCardAssignRequest
    {
        public int? CardId { get; set; }
        public int? UserId { get; set; }
    }
}
