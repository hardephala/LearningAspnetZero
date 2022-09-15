using Abp.Application.Services.Dto;

namespace Arch.Invoicedatas.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}