using Abp.Application.Services.Dto;
using System;

namespace Arch.Donotreleases.Dtos
{
    public class GetAllDonotreleasesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string blnoFilter { get; set; }

        public string statusFilter { get; set; }

        public string releasedbyFilter { get; set; }

        public string releasecommentFilter { get; set; }

        public string blockedbyFilter { get; set; }

        public string blockedcommentFilter { get; set; }

        public DateTime? MaxblockeddateFilter { get; set; }
        public DateTime? MinblockeddateFilter { get; set; }

        public string blockedreferenceFilter { get; set; }

        public string blcommentFilter { get; set; }

    }
}