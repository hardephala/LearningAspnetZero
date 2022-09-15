using Abp.Application.Services.Dto;
using System;

namespace Arch.Cargoreleases.Dtos
{
    public class GetAllCargoreleasesForExcelInput
    {
        public string Filter { get; set; }

        public string priorityFilter { get; set; }

        public string blnoFilter { get; set; }

        public DateTime? MaxinvoicevalidityFilter { get; set; }
        public DateTime? MininvoicevalidityFilter { get; set; }

        public string terminalFilter { get; set; }

        public string deliveryordernoFilter { get; set; }

        public string customercodeFilter { get; set; }

        public string agencycodeFilter { get; set; }

        public string agentcodeFilter { get; set; }

        public string entrybyrepcodeFilter { get; set; }

        public string entrymodeFilter { get; set; }

        public DateTime? MaxentrydateFilter { get; set; }
        public DateTime? MinentrydateFilter { get; set; }

        public string approvebyFilter { get; set; }

        public string approvecommentFilter { get; set; }

        public DateTime? MaxapprovedateFilter { get; set; }
        public DateTime? MinapprovedateFilter { get; set; }

        public string updatedbyFilter { get; set; }

        public string updatecommentFilter { get; set; }

        public DateTime? MaxupdatedateFilter { get; set; }
        public DateTime? MinupdatedateFilter { get; set; }

        public string releasebyFilter { get; set; }

        public string releasestatusFilter { get; set; }

        public string releasecommentFilter { get; set; }

        public DateTime? MaxreleasedateFilter { get; set; }
        public DateTime? MinreleasedateFilter { get; set; }

        public string statusFilter { get; set; }

        public string ipaddrFilter { get; set; }

    }
}