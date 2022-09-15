using Abp.Application.Services.Dto;
using System;

namespace Arch.Longstandings.Dtos
{
    public class GetAllLongstandingsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string customercodeFilter { get; set; }

        public string blnoFilter { get; set; }

        public string containernoFilter { get; set; }

        public string containertypeFilter { get; set; }

        public string freetextFilter { get; set; }

        public string locationFilter { get; set; }

        public string lastmoveFilter { get; set; }

        public int? MaxdaysFilter { get; set; }
        public int? MindaysFilter { get; set; }

        public string statusFilter { get; set; }

        public string releasedbyFilter { get; set; }

        public string releasedreasonFilter { get; set; }

        public string releasecommentFilter { get; set; }

        public DateTime? MaxvaliditydateFilter { get; set; }
        public DateTime? MinvaliditydateFilter { get; set; }

        public string shipoperatorFilter { get; set; }

    }
}