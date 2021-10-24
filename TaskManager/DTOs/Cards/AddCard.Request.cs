using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Cards
{
    public class AddCardRequest
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }
}
