using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.ProjectMembers;
using TM.Domain.Entities.ProjectPhases;

#nullable disable

namespace TM.Domain.Entities.Projects
{
    [Table("Project")]
    public partial class Project : AuditEntity<int>
    {
        public Project()
        {
            Cards = new HashSet<Card>();
            ProjectMembers = new HashSet<ProjectMember>();
            ProjectPhases = new HashSet<ProjectPhase>();
        }
        [StringLength(50)]
        public string Name { get; set; }
        

        [InverseProperty(nameof(Card.Project))]
        public virtual ICollection<Card> Cards { get; set; }
        [InverseProperty(nameof(ProjectMember.Project))]
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }
        [InverseProperty(nameof(ProjectPhase.Project))]
        public virtual ICollection<ProjectPhase> ProjectPhases { get; set; }
    }
}
