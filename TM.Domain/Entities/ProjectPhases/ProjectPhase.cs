using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.Phases;
using TM.Domain.Entities.Projects;

#nullable disable

namespace TM.Domain.Entities.ProjectPhases
{
    [Table("ProjectPhase")]
    public partial class ProjectPhase : EntityBase<int>
    {
        public int? ProjectId { get; set; }
        public int? PhaseId { get; set; }

        [ForeignKey(nameof(PhaseId))]
        [InverseProperty("ProjectPhases")]
        public virtual Phase Phase { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectPhases")]
        public virtual Project Project { get; set; }
    }
}
