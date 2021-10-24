using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.ProjectMembers;
using TM.Domain.Entities.ProjectPhases;
using TM.Domain.Entities.Users;
using TM.Domain.Shared;

namespace TM.Domain.Entities.Projects
{
    public partial class Project
    {
        public Project(string name) : base()
        {
            Name = name;
        }


        public bool ValidOnAdd()
        {
            return
                !string.IsNullOrEmpty(Name);
        }

        public void AddProjectMember(User user)
        {
            if (ProjectMembers == null)
                ProjectMembers = new List<ProjectMember>() {
                    new ProjectMember(user, this)
                };
            else
            {
                ProjectMembers.Add(new ProjectMember(user, this));
            }
        }
        public void AddBasicPhases()
        {
            this.ProjectPhases = new List<ProjectPhase>()
                {
                    new ProjectPhase() { ProjectId = Id, PhaseId = (int)PhaseBasic.Opportunity},
                    new ProjectPhase() { ProjectId = Id, PhaseId = (int)PhaseBasic.Quote},
                    new ProjectPhase() { ProjectId = Id, PhaseId = (int)PhaseBasic.Order},
                    new ProjectPhase() { ProjectId = Id, PhaseId = (int)PhaseBasic.Completed},
                    new ProjectPhase() { ProjectId = Id, PhaseId = (int)PhaseBasic.Destroy},
                };
        }

        public void AddCard(Card cardMovement)
        {
            if (this.Cards != null)
                this.Cards.Add(cardMovement);
            else
                this.Cards = new List<Card>() { cardMovement };
        }
    }
}
