using Arch.ErInvoiceDatas.Dtos;

using Abp.Extensions;

namespace Arch.Web.Areas.App.Models.ErInvoiceDatas
{
    public class CreateOrEditErInvoiceDataModalViewModel
    {
        public CreateOrEditErInvoiceDataDto ErInvoiceData { get; set; }

        public string Billofladingblno { get; set; }

        public bool IsEditMode => ErInvoiceData.Id.HasValue;
    }
}