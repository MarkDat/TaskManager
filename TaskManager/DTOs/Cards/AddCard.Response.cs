using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Cards
{
    public class AddCardResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProjectId { get; set; }
    }
}
