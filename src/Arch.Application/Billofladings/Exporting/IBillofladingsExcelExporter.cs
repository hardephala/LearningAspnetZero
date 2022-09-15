using System.Collections.Generic;
using Arch.Billofladings.Dtos;
using Arch.Dto;

namespace Arch.Billofladings.Exporting
{
    public interface IBillofladingsExcelExporter
    {
        FileDto ExportToFile(List<GetBillofladingForViewDto> billofladings);
    }
}