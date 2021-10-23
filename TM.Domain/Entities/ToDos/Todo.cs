using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.Cards;

#nullable disable

namespace TM.Domain.Entities.ToDos
{
    [Table("Todo")]
    public partial class Todo : AuditEntity<int>
    {
        public Todo()
        {
            InverseParent = new HashSet<Todo>();
        }

        [StringLength(50)]
        public string Name { get; set; }
        public bool? IsCheck { get; set; }
        
        public int? ParentId { get; set; }
        public int? CardId { get; set; }

        [ForeignKey(nameof(CardId))]
        [InverseProperty("Todos")]
        public virtual Card Card { get; set; }
        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(Todo.InverseParent))]
        public virtual Todo Parent { get; set; }
        [InverseProperty(nameof(Todo.Parent))]
        public virtual ICollection<Todo> InverseParent { get; set; }
    }
}
