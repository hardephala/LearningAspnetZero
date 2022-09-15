using Arch.Billofladings;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Arch.ErInvoiceDatas
{
    [Table("ErInvoiceDatas")]
    [Audited]
    public class ErInvoiceData : AuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual DateTime? validityDate { get; set; }

        public virtual string amount { get; set; }

        public virtual string amountdue { get; set; }

        public virtual string status { get; set; }

        public virtual long? BillofladingId { get; set; }

        [ForeignKey("BillofladingId")]
        public Billoflading BillofladingFk { get; set; }

    }
}