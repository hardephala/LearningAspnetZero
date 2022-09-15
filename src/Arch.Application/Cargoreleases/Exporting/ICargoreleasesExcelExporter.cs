using System.Collections.Generic;
using Arch.Cargoreleases.Dtos;
using Arch.Dto;

namespace Arch.Cargoreleases.Exporting
{
    public interface ICargoreleasesExcelExporter
    {
        FileDto ExportToFile(List<GetCargoreleaseForViewDto> cargoreleases);
    }
}