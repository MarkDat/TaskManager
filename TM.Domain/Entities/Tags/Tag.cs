using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.CardTags;

#nullable disable

namespace TM.Domain.Entities.Tags
{
    [Table("Tag")]
    public partial class Tag : EntityBase<int>
    {
        public Tag()
        {
            CardTags = new HashSet<CardTag>();
        }

        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Color { get; set; }

        [InverseProperty(nameof(CardTag.Tag))]
        public virtual ICollection<CardTag> CardTags { get; set; }
    }
}
