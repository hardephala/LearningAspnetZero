using Abp.Application.Services.Dto;

namespace Arch.Billofladings.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}