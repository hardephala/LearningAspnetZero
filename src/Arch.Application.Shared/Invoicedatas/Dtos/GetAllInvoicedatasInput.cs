using Abp.Application.Services.Dto;
using System;

namespace Arch.Invoicedatas.Dtos
{
    public class GetAllInvoicedatasInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string blnoFilter { get; set; }

        public DateTime? MaxvaliditydateFilter { get; set; }
        public DateTime? MinvaliditydateFilter { get; set; }

        public string amountFilter { get; set; }

        public string amountdueFilter { get; set; }

        public string statusFilter { get; set; }

        public DateTime? MaxinvpaiddateFilter { get; set; }
        public DateTime? MininvpaiddateFilter { get; set; }

        public string useridFilter { get; set; }

        public string waiverFilter { get; set; }

        public string waivedamountFilter { get; set; }

        public string waivedbyFilter { get; set; }

        public string waivecommentFilter { get; set; }

        public DateTime? MaxdatewaivedFilter { get; set; }
        public DateTime? MindatewaivedFilter { get; set; }

        public DateTime? MaxCreationTimeFilter { get; set; }
        public DateTime? MinCreationTimeFilter { get; set; }

        public DateTime? MaxLastModificationTimeFilter { get; set; }
        public DateTime? MinLastModificationTimeFilter { get; set; }

        public string BillofladingblnoFilter { get; set; }

    }
}