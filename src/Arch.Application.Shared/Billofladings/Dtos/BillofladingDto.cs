using System;
using Abp.Application.Services.Dto;

namespace Arch.Billofladings.Dtos
{
    public class BillofladingDto : EntityDto<long>
    {
        public string shipmentno { get; set; }

        public string blno { get; set; }

        public string equipmentno { get; set; }

        public string equipmenttype { get; set; }

        public string equipmentsize { get; set; }

        public string shipperowned { get; set; }

        public string shipoperator { get; set; }

        public string servicecontract { get; set; }

        public string spotbooking { get; set; }

        public string consigneecode { get; set; }

        public string dischargeportcode { get; set; }

        public string dischargeportname { get; set; }

        public string placeofdeliverycode { get; set; }

        public string placeofdeliveryname { get; set; }

        public string finalvesselcode { get; set; }

        public string finalvesselname { get; set; }

        public string finalvesselvoyage { get; set; }

        public DateTime finalvesseleta { get; set; }

        public string partpartstatus { get; set; }

        public string partpartref { get; set; }

        public string depositpayable { get; set; }

        public int depositdueamount { get; set; }

        public string depositwaivedamount { get; set; }

        public string depositwaivedreason { get; set; }

        public string depositwaivedby { get; set; }

        public string depositpaymentstatus { get; set; }

        public string releaseoutstandingstatus { get; set; }

        public string releaseoutstandingreason { get; set; }

        public string releaseoutstandingby { get; set; }

        public string releaselongstandingstatus { get; set; }

        public string releaselongstandingreason { get; set; }

        public string releaselongstandingby { get; set; }

        public string blnotype { get; set; }

        public string blnosubmitstatus { get; set; }

        public string blnosubmittedto { get; set; }

        public string blnosubmittedby { get; set; }

        public string blnosubmitref { get; set; }

    }
}