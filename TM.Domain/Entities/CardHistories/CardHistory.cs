using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Base;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.Users;

#nullable disable

namespace TM.Domain.Entities.CardHistories
{
    [Table("CardHistory")]
    public partial class CardHistory : AuditEntity<int>
    {
        [StringLength(200)]
        public string Content { get; set; }
        public int? CardId { get; set; }
        
        [StringLength(25)]
        public string ActionType { get; set; }
       
        public int? UserId { get; set; }

        [ForeignKey(nameof(CardId))]
        [InverseProperty("CardHistories")]
        public virtual Card Card { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("CardHistories")]
        public virtual User User { get; set; }
    }
}
