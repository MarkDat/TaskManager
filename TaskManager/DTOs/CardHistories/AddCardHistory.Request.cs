using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.CardHistories
{
    public class AddCardHistoryRequest
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int? CardId { get; set; }

        public string Content { get; set; }
        public string ActionType { get; set; }
    }
}
