using System.Collections.Generic;
using Arch.Invoicedatas.Dtos;
using Arch.Dto;

namespace Arch.Invoicedatas.Exporting
{
    public interface IInvoicedatasExcelExporter
    {
        FileDto ExportToFile(List<GetInvoicedataForViewDto> invoicedatas);
    }
}