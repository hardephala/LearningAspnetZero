using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.ErInvoiceDatas.Dtos
{
    public class CreateOrEditErInvoiceDataDto : EntityDto<int?>
    {

        public DateTime? validityDate { get; set; }

        public string amount { get; set; }

        public string amountdue { get; set; }

        public string status { get; set; }

        public long? BillofladingId { get; set; }

    }
}