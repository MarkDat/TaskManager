using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Priority
{
    public class GetPriorityResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
