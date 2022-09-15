using Abp.Application.Services.Dto;

namespace Arch.Cargoreleases.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}