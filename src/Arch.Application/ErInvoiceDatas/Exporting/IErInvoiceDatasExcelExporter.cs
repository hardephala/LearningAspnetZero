using System.Collections.Generic;
using Arch.ErInvoiceDatas.Dtos;
using Arch.Dto;

namespace Arch.ErInvoiceDatas.Exporting
{
    public interface IErInvoiceDatasExcelExporter
    {
        FileDto ExportToFile(List<GetErInvoiceDataForViewDto> erInvoiceDatas);
    }
}