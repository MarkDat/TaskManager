using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.Base
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }

    

    public interface IAuditEntity
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    public interface IAuditEntity<TKey> : IAuditEntity
    {
    }

    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public virtual TKey Id { get; set; }
    }


    public abstract class AuditEntity<TKey> : EntityBase<TKey>, IAuditEntity<TKey>
    {
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(25)]
        public string CreatedBy { get; set; }
        [StringLength(25)]
        public string UpdatedBy { get; set; }
    }
}
