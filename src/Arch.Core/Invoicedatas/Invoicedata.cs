using Arch.Billofladings;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Arch.Invoicedatas
{
    [Table("invoicedatas")]
    [Audited]
    public class Invoicedata : AuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(InvoicedataConsts.MaxblnoLength, MinimumLength = InvoicedataConsts.MinblnoLength)]
        public virtual string blno { get; set; }

        [Required]
        public virtual DateTime validitydate { get; set; }

        [StringLength(InvoicedataConsts.MaxamountLength, MinimumLength = InvoicedataConsts.MinamountLength)]
        public virtual string amount { get; set; }

        [StringLength(InvoicedataConsts.MaxamountdueLength, MinimumLength = InvoicedataConsts.MinamountdueLength)]
        public virtual string amountdue { get; set; }

        [StringLength(InvoicedataConsts.MaxstatusLength, MinimumLength = InvoicedataConsts.MinstatusLength)]
        public virtual string status { get; set; }

        [Required]
        public virtual DateTime invpaiddate { get; set; }

        [StringLength(InvoicedataConsts.MaxuseridLength, MinimumLength = InvoicedataConsts.MinuseridLength)]
        public virtual string userid { get; set; }

        [StringLength(InvoicedataConsts.MaxwaiverLength, MinimumLength = InvoicedataConsts.MinwaiverLength)]
        public virtual string waiver { get; set; }

        [StringLength(InvoicedataConsts.MaxwaivedamountLength, MinimumLength = InvoicedataConsts.MinwaivedamountLength)]
        public virtual string waivedamount { get; set; }

        [StringLength(InvoicedataConsts.MaxwaivedbyLength, MinimumLength = InvoicedataConsts.MinwaivedbyLength)]
        public virtual string waivedby { get; set; }

        [StringLength(InvoicedataConsts.MaxwaivecommentLength, MinimumLength = InvoicedataConsts.MinwaivecommentLength)]
        public virtual string waivecomment { get; set; }

        [Required]
        public virtual DateTime datewaived { get; set; }

        [Required]
        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? BillofladingId { get; set; }

        [ForeignKey("BillofladingId")]
        public Billoflading BillofladingFk { get; set; }

    }
}