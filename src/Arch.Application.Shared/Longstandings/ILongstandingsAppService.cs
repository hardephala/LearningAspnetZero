using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Arch.Longstandings.Dtos;
using Arch.Dto;

namespace Arch.Longstandings
{
    public interface ILongstandingsAppService : IApplicationService
    {
        Task<PagedResultDto<GetLongstandingForViewDto>> GetAll(GetAllLongstandingsInput input);

        Task<GetLongstandingForViewDto> GetLongstandingForView(int id);

        Task<GetLongstandingForEditOutput> GetLongstandingForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditLongstandingDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetLongstandingsToExcel(GetAllLongstandingsForExcelInput input);

    }
}