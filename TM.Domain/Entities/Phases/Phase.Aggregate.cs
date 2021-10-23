using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.ProjectPhases;
using TM.Domain.Shared;

namespace TM.Domain.Entities.Phases
{
    public partial class Phase
    {
        public Phase(string name,int? acceptMoveId) : base()
        {
            Name = name;
            AcceptMoveId = acceptMoveId;
        }


        public bool ValidOnAdd()
        {
            return
                !string.IsNullOrEmpty(Name);
        }
    }
}
