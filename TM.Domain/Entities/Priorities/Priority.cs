using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.Cards;

#nullable disable

namespace TM.Domain.Entities.Priorities
{
    [Table("Priority")]
    public partial class Priority : EntityBase<int>
    {
        public Priority()
        {
            Cards = new HashSet<Card>();
        }

        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(15)]
        public string Color { get; set; }

        [InverseProperty(nameof(Card.Priority))]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
