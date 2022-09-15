using Abp.Application.Services.Dto;

namespace Arch.Donotreleases.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}