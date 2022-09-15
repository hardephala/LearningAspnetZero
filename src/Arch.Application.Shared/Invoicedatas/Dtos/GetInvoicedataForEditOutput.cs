using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Invoicedatas.Dtos
{
    public class GetInvoicedataForEditOutput
    {
        public CreateOrEditInvoicedataDto Invoicedata { get; set; }

        public string Billofladingblno { get; set; }

    }
}