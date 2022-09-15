using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Arch.ErInvoiceDatas.Dtos;
using Arch.Dto;

namespace Arch.ErInvoiceDatas
{
    public interface IErInvoiceDatasAppService : IApplicationService
    {
        Task<PagedResultDto<GetErInvoiceDataForViewDto>> GetAll(GetAllErInvoiceDatasInput input);

        Task<GetErInvoiceDataForViewDto> GetErInvoiceDataForView(int id);

        Task<GetErInvoiceDataForEditOutput> GetErInvoiceDataForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditErInvoiceDataDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetErInvoiceDatasToExcel(GetAllErInvoiceDatasForExcelInput input);

        Task<PagedResultDto<ErInvoiceDataBillofladingLookupTableDto>> GetAllBillofladingForLookupTable(GetAllForLookupTableInput input);

    }
}