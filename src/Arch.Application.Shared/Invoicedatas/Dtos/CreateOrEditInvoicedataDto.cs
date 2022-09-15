using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Invoicedatas.Dtos
{
    public class CreateOrEditInvoicedataDto : EntityDto<int?>
    {

        [StringLength(InvoicedataConsts.MaxblnoLength, MinimumLength = InvoicedataConsts.MinblnoLength)]
        public string blno { get; set; }

        [Required]
        public DateTime validitydate { get; set; }

        [StringLength(InvoicedataConsts.MaxamountLength, MinimumLength = InvoicedataConsts.MinamountLength)]
        public string amount { get; set; }

        [StringLength(InvoicedataConsts.MaxamountdueLength, MinimumLength = InvoicedataConsts.MinamountdueLength)]
        public string amountdue { get; set; }

        [StringLength(InvoicedataConsts.MaxstatusLength, MinimumLength = InvoicedataConsts.MinstatusLength)]
        public string status { get; set; }

        [Required]
        public DateTime invpaiddate { get; set; }

        [StringLength(InvoicedataConsts.MaxuseridLength, MinimumLength = InvoicedataConsts.MinuseridLength)]
        public string userid { get; set; }

        [StringLength(InvoicedataConsts.MaxwaiverLength, MinimumLength = InvoicedataConsts.MinwaiverLength)]
        public string waiver { get; set; }

        [StringLength(InvoicedataConsts.MaxwaivedamountLength, MinimumLength = InvoicedataConsts.MinwaivedamountLength)]
        public string waivedamount { get; set; }

        [StringLength(InvoicedataConsts.MaxwaivedbyLength, MinimumLength = InvoicedataConsts.MinwaivedbyLength)]
        public string waivedby { get; set; }

        [StringLength(InvoicedataConsts.MaxwaivecommentLength, MinimumLength = InvoicedataConsts.MinwaivecommentLength)]
        public string waivecomment { get; set; }

        [Required]
        public DateTime datewaived { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public long? BillofladingId { get; set; }

    }
}