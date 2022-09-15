using Abp.Application.Services.Dto;

namespace Arch.ErInvoiceDatas.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}