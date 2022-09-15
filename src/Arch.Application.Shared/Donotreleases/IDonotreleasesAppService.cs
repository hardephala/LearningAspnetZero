using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Arch.Donotreleases.Dtos;
using Arch.Dto;

namespace Arch.Donotreleases
{
    public interface IDonotreleasesAppService : IApplicationService
    {
        Task<PagedResultDto<GetDonotreleaseForViewDto>> GetAll(GetAllDonotreleasesInput input);

        Task<GetDonotreleaseForViewDto> GetDonotreleaseForView(int id);

        Task<GetDonotreleaseForEditOutput> GetDonotreleaseForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditDonotreleaseDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetDonotreleasesToExcel(GetAllDonotreleasesForExcelInput input);

    }
}