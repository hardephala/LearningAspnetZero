using System;
using Abp.Application.Services.Dto;

namespace Arch.Invoicedatas.Dtos
{
    public class InvoicedataDto : EntityDto
    {
        public string blno { get; set; }

        public DateTime validitydate { get; set; }

        public string amount { get; set; }

        public string amountdue { get; set; }

        public string status { get; set; }

        public DateTime invpaiddate { get; set; }

        public string userid { get; set; }

        public string waiver { get; set; }

        public string waivedamount { get; set; }

        public string waivedby { get; set; }

        public string waivecomment { get; set; }

        public DateTime datewaived { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public long? BillofladingId { get; set; }

    }
}