using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.CardAssigns;
using TM.Domain.Entities.CardHistories;
using TM.Domain.Entities.CardMovements;
using TM.Domain.Entities.CardTags;
using TM.Domain.Entities.Priorities;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.ToDos;

#nullable disable

namespace TM.Domain.Entities.Cards
{
    [Table("Card")]
    public partial class Card : AuditEntity<int>
    {
        public Card()
        {
            CardAssigns = new HashSet<CardAssign>();
            CardHistories = new HashSet<CardHistory>();
            CardMovements = new HashSet<CardMovement>();
            CardTags = new HashSet<CardTag>();
            Todos = new HashSet<Todo>();
        }

        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
       
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        public int? ProjectId { get; set; }
        public int? PriorityId { get; set; }

        [ForeignKey(nameof(PriorityId))]
        [InverseProperty("Cards")]
        public virtual Priority Priority { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("Cards")]
        public virtual Project Project { get; set; }
        [InverseProperty(nameof(CardAssign.Card))]
        public virtual ICollection<CardAssign> CardAssigns { get; set; }
        [InverseProperty(nameof(CardHistory.Card))]
        public virtual ICollection<CardHistory> CardHistories { get; set; }
        [InverseProperty(nameof(CardMovement.Card))]
        public virtual ICollection<CardMovement> CardMovements { get; set; }
        [InverseProperty(nameof(CardTag.Card))]
        public virtual ICollection<CardTag> CardTags { get; set; }
        [InverseProperty(nameof(Todo.Card))]
        public virtual ICollection<Todo> Todos { get; set; }
    }
}
