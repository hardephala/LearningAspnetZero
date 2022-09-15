using System;
using Abp.Application.Services.Dto;

namespace Arch.ErInvoiceDatas.Dtos
{
    public class ErInvoiceDataDto : EntityDto
    {
        public DateTime? validityDate { get; set; }

        public string amount { get; set; }

        public string amountdue { get; set; }

        public string status { get; set; }

        public long? BillofladingId { get; set; }

    }
}