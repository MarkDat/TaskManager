using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.CardAssigns;
using TM.Domain.Entities.CardHistories;
using TM.Domain.Entities.ProjectMembers;

#nullable disable

namespace TM.Domain.Entities.Users
{
    [Table("User")]
    public partial class User : EntityBase<int>
    {
        public User()
        {
            CardAssigns = new HashSet<CardAssign>();
            CardHistories = new HashSet<CardHistory>();
            ProjectMembers = new HashSet<ProjectMember>();
        }

        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        [Column(TypeName = "text")]
        public string Image { get; set; }
        [StringLength(25)]
        public string FirstName { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Birthday { get; set; }

        [InverseProperty(nameof(CardAssign.User))]
        public virtual ICollection<CardAssign> CardAssigns { get; set; }
        [InverseProperty(nameof(CardHistory.User))]
        public virtual ICollection<CardHistory> CardHistories { get; set; }
        [InverseProperty(nameof(ProjectMember.User))]
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }
    }
}
