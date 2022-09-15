using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Arch.Invoicedatas.Dtos;
using Arch.Dto;
using System.Collections.Generic;

namespace Arch.Invoicedatas
{
    public interface IInvoicedatasAppService : IApplicationService
    {
        Task<PagedResultDto<GetInvoicedataForViewDto>> GetAll(GetAllInvoicedatasInput input);

        Task<GetInvoicedataForViewDto> GetInvoicedataForView(int id);

        Task<GetInvoicedataForEditOutput> GetInvoicedataForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditInvoicedataDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetInvoicedatasToExcel(GetAllInvoicedatasForExcelInput input);

        Task<List<InvoicedataBillofladingLookupTableDto>> GetAllBillofladingForTableDropdown();

    }
}