using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.Shared
{
   public enum PhaseBasic
    {
        Destroy = 1,
        Completed,
        Order,
        Quote,
        Opportunity
    }

    public enum PriorityBasic
    {
        Medium = 1,
        Emergency = 2
    }

    public enum HistoryActionType
    {
        Added,
        Updated,
        Deleted,
        Assign,
        Move
    }

    public enum AcionCard
    {
        Add,
        Update,
        Delete
    }
}
