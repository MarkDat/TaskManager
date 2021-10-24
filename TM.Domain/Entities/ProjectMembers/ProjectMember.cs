using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.Users;

#nullable disable

namespace TM.Domain.Entities.ProjectMembers
{
    [Table("ProjectMember")]
    public partial class ProjectMember : EntityBase<int>
    {
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOwner { get; set; }

        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectMembers")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ProjectMembers")]
        public virtual User User { get; set; }
    }
}
