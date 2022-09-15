using System.Collections.Generic;
using Arch.Donotreleases.Dtos;
using Arch.Dto;

namespace Arch.Donotreleases.Exporting
{
    public interface IDonotreleasesExcelExporter
    {
        FileDto ExportToFile(List<GetDonotreleaseForViewDto> donotreleases);
    }
}