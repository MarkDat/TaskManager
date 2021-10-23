using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Cards
{
    public class GetCardRequest
    {
        public int IdProject { get; set; }
        public string Name { get; set; }
    }
}
