using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.CardMovements;
using TM.Domain.Entities.ProjectPhases;

#nullable disable

namespace TM.Domain.Entities.Phases
{
    [Table("Phase")]
    public partial class Phase : EntityBase<int>
    {
        public Phase()
        {
            CardMovements = new HashSet<CardMovement>();
            ProjectPhases = new HashSet<ProjectPhase>();
        }

       
        [StringLength(50)]
        public string Name { get; set; }
        public int? AcceptMoveId { get; set; }

        [InverseProperty(nameof(CardMovement.Phase))]
        public virtual ICollection<CardMovement> CardMovements { get; set; }
        [InverseProperty(nameof(ProjectPhase.Phase))]
        public virtual ICollection<ProjectPhase> ProjectPhases { get; set; }
    }
}
