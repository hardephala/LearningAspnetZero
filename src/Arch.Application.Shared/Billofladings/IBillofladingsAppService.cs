using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Arch.Billofladings.Dtos;
using Arch.Dto;

namespace Arch.Billofladings
{
    public interface IBillofladingsAppService : IApplicationService
    {
        Task<PagedResultDto<GetBillofladingForViewDto>> GetAll(GetAllBillofladingsInput input);

        Task<GetBillofladingForViewDto> GetBillofladingForView(long id);

        Task<GetBillofladingForEditOutput> GetBillofladingForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditBillofladingDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetBillofladingsToExcel(GetAllBillofladingsForExcelInput input);

    }
}