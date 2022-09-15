using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Arch.Cargoreleases.Dtos;
using Arch.Dto;

namespace Arch.Cargoreleases
{
    public interface ICargoreleasesAppService : IApplicationService
    {
        Task<PagedResultDto<GetCargoreleaseForViewDto>> GetAll(GetAllCargoreleasesInput input);

        Task<GetCargoreleaseForViewDto> GetCargoreleaseForView(long id);

        Task<GetCargoreleaseForEditOutput> GetCargoreleaseForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditCargoreleaseDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetCargoreleasesToExcel(GetAllCargoreleasesForExcelInput input);

    }
}