using Abp.Application.Services.Dto;

namespace Arch.Longstandings.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}