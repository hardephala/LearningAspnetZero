using Abp.Application.Services.Dto;

namespace Arch.Customers.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}