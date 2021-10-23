using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.Tags;

#nullable disable

namespace TM.Domain.Entities.CardTags
{
    [Table("CardTag")]
    public partial class CardTag : EntityBase<int>
    {
        public int? CardId { get; set; }
        public int? TagId { get; set; }

        [ForeignKey(nameof(CardId))]
        [InverseProperty("CardTags")]
        public virtual Card Card { get; set; }
        [ForeignKey(nameof(TagId))]
        [InverseProperty("CardTags")]
        public virtual Tag Tag { get; set; }
    }
}
