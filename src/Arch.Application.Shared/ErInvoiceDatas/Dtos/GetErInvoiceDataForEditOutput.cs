using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.ErInvoiceDatas.Dtos
{
    public class GetErInvoiceDataForEditOutput
    {
        public CreateOrEditErInvoiceDataDto ErInvoiceData { get; set; }

        public string Billofladingblno { get; set; }

    }
}