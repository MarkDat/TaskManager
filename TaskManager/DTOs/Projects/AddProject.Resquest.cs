using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Projects
{
    public class AddProjectRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
