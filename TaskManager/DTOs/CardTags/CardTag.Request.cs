using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.CardTags
{
    public class CardTagRequest
    {
        public int? CardId { get; set; }
        public int? TagId { get; set; }
    }
}
