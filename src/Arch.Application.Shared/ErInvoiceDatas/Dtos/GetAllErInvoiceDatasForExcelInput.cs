using Abp.Application.Services.Dto;
using System;

namespace Arch.ErInvoiceDatas.Dtos
{
    public class GetAllErInvoiceDatasForExcelInput
    {
        public string Filter { get; set; }

        public DateTime? MaxvalidityDateFilter { get; set; }
        public DateTime? MinvalidityDateFilter { get; set; }

        public string amountFilter { get; set; }

        public string amountdueFilter { get; set; }

        public string statusFilter { get; set; }

        public string BillofladingblnoFilter { get; set; }

    }
}