using System.Collections.Generic;
using Arch.Longstandings.Dtos;
using Arch.Dto;

namespace Arch.Longstandings.Exporting
{
    public interface ILongstandingsExcelExporter
    {
        FileDto ExportToFile(List<GetLongstandingForViewDto> longstandings);
    }
}