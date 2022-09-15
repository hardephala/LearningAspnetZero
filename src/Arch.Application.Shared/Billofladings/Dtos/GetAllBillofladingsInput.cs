using Abp.Application.Services.Dto;
using System;

namespace Arch.Billofladings.Dtos
{
    public class GetAllBillofladingsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string shipmentnoFilter { get; set; }

        public string blnoFilter { get; set; }

        public string equipmentnoFilter { get; set; }

        public string equipmenttypeFilter { get; set; }

        public string equipmentsizeFilter { get; set; }

        public string shipperownedFilter { get; set; }

        public string shipoperatorFilter { get; set; }

        public string servicecontractFilter { get; set; }

        public string spotbookingFilter { get; set; }

        public string consigneecodeFilter { get; set; }

        public string dischargeportcodeFilter { get; set; }

        public string dischargeportnameFilter { get; set; }

        public string placeofdeliverycodeFilter { get; set; }

        public string placeofdeliverynameFilter { get; set; }

        public string finalvesselcodeFilter { get; set; }

        public string finalvesselnameFilter { get; set; }

        public string finalvesselvoyageFilter { get; set; }

        public DateTime? MaxfinalvesseletaFilter { get; set; }
        public DateTime? MinfinalvesseletaFilter { get; set; }

        public string partpartstatusFilter { get; set; }

        public string partpartrefFilter { get; set; }

        public string depositpayableFilter { get; set; }

        public int? MaxdepositdueamountFilter { get; set; }
        public int? MindepositdueamountFilter { get; set; }

        public string depositwaivedamountFilter { get; set; }

        public string depositwaivedreasonFilter { get; set; }

        public string depositwaivedbyFilter { get; set; }

        public string depositpaymentstatusFilter { get; set; }

        public string releaseoutstandingstatusFilter { get; set; }

        public string releaseoutstandingreasonFilter { get; set; }

        public string releaseoutstandingbyFilter { get; set; }

        public string releaselongstandingstatusFilter { get; set; }

        public string releaselongstandingreasonFilter { get; set; }

        public string releaselongstandingbyFilter { get; set; }

        public string blnotypeFilter { get; set; }

        public string blnosubmitstatusFilter { get; set; }

        public string blnosubmittedtoFilter { get; set; }

        public string blnosubmittedbyFilter { get; set; }

        public string blnosubmitrefFilter { get; set; }

    }
}