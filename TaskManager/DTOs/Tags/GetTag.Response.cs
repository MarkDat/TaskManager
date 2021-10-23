using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Tags
{
    public class GetTagResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
