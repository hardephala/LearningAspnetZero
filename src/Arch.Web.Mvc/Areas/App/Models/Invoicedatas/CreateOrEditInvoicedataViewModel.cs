using Arch.Invoicedatas.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace Arch.Web.Areas.App.Models.Invoicedatas
{
    public class CreateOrEditInvoicedataModalViewModel
    {
        public CreateOrEditInvoicedataDto Invoicedata { get; set; }

        public string Billofladingblno { get; set; }

        public List<InvoicedataBillofladingLookupTableDto> InvoicedataBillofladingList { get; set; }

        public bool IsEditMode => Invoicedata.Id.HasValue;
    }
}