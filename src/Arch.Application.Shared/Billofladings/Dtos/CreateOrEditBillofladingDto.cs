using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Billofladings.Dtos
{
    public class CreateOrEditBillofladingDto : EntityDto<long?>
    {

        [StringLength(BillofladingConsts.MaxshipmentnoLength, MinimumLength = BillofladingConsts.MinshipmentnoLength)]
        public string shipmentno { get; set; }

        [StringLength(BillofladingConsts.MaxblnoLength, MinimumLength = BillofladingConsts.MinblnoLength)]
        public string blno { get; set; }

        [StringLength(BillofladingConsts.MaxequipmentnoLength, MinimumLength = BillofladingConsts.MinequipmentnoLength)]
        public string equipmentno { get; set; }

        [StringLength(BillofladingConsts.MaxequipmenttypeLength, MinimumLength = BillofladingConsts.MinequipmenttypeLength)]
        public string equipmenttype { get; set; }

        [StringLength(BillofladingConsts.MaxequipmentsizeLength, MinimumLength = BillofladingConsts.MinequipmentsizeLength)]
        public string equipmentsize { get; set; }

        [StringLength(BillofladingConsts.MaxshipperownedLength, MinimumLength = BillofladingConsts.MinshipperownedLength)]
        public string shipperowned { get; set; }

        [StringLength(BillofladingConsts.MaxshipoperatorLength, MinimumLength = BillofladingConsts.MinshipoperatorLength)]
        public string shipoperator { get; set; }

        [StringLength(BillofladingConsts.MaxservicecontractLength, MinimumLength = BillofladingConsts.MinservicecontractLength)]
        public string servicecontract { get; set; }

        [StringLength(BillofladingConsts.MaxspotbookingLength, MinimumLength = BillofladingConsts.MinspotbookingLength)]
        public string spotbooking { get; set; }

        [StringLength(BillofladingConsts.MaxconsigneecodeLength, MinimumLength = BillofladingConsts.MinconsigneecodeLength)]
        public string consigneecode { get; set; }

        [StringLength(BillofladingConsts.MaxdischargeportcodeLength, MinimumLength = BillofladingConsts.MindischargeportcodeLength)]
        public string dischargeportcode { get; set; }

        public string dischargeportname { get; set; }

        [StringLength(BillofladingConsts.MaxplaceofdeliverycodeLength, MinimumLength = BillofladingConsts.MinplaceofdeliverycodeLength)]
        public string placeofdeliverycode { get; set; }

        [StringLength(BillofladingConsts.MaxplaceofdeliverynameLength, MinimumLength = BillofladingConsts.MinplaceofdeliverynameLength)]
        public string placeofdeliveryname { get; set; }

        [StringLength(BillofladingConsts.MaxfinalvesselcodeLength, MinimumLength = BillofladingConsts.MinfinalvesselcodeLength)]
        public string finalvesselcode { get; set; }

        [StringLength(BillofladingConsts.MaxfinalvesselnameLength, MinimumLength = BillofladingConsts.MinfinalvesselnameLength)]
        public string finalvesselname { get; set; }

        [StringLength(BillofladingConsts.MaxfinalvesselvoyageLength, MinimumLength = BillofladingConsts.MinfinalvesselvoyageLength)]
        public string finalvesselvoyage { get; set; }

        public DateTime finalvesseleta { get; set; }

        [StringLength(BillofladingConsts.MaxpartpartstatusLength, MinimumLength = BillofladingConsts.MinpartpartstatusLength)]
        public string partpartstatus { get; set; }

        [StringLength(BillofladingConsts.MaxpartpartrefLength, MinimumLength = BillofladingConsts.MinpartpartrefLength)]
        public string partpartref { get; set; }

        [StringLength(BillofladingConsts.MaxdepositpayableLength, MinimumLength = BillofladingConsts.MindepositpayableLength)]
        public string depositpayable { get; set; }

        [Range(BillofladingConsts.MindepositdueamountValue, BillofladingConsts.MaxdepositdueamountValue)]
        public int depositdueamount { get; set; }

        [StringLength(BillofladingConsts.MaxdepositwaivedamountLength, MinimumLength = BillofladingConsts.MindepositwaivedamountLength)]
        public string depositwaivedamount { get; set; }

        [StringLength(BillofladingConsts.MaxdepositwaivedreasonLength, MinimumLength = BillofladingConsts.MindepositwaivedreasonLength)]
        public string depositwaivedreason { get; set; }

        [StringLength(BillofladingConsts.MaxdepositwaivedbyLength, MinimumLength = BillofladingConsts.MindepositwaivedbyLength)]
        public string depositwaivedby { get; set; }

        [StringLength(BillofladingConsts.MaxdepositpaymentstatusLength, MinimumLength = BillofladingConsts.MindepositpaymentstatusLength)]
        public string depositpaymentstatus { get; set; }

        [StringLength(BillofladingConsts.MaxreleaseoutstandingstatusLength, MinimumLength = BillofladingConsts.MinreleaseoutstandingstatusLength)]
        public string releaseoutstandingstatus { get; set; }

        [StringLength(BillofladingConsts.MaxreleaseoutstandingreasonLength, MinimumLength = BillofladingConsts.MinreleaseoutstandingreasonLength)]
        public string releaseoutstandingreason { get; set; }

        [StringLength(BillofladingConsts.MaxreleaseoutstandingbyLength, MinimumLength = BillofladingConsts.MinreleaseoutstandingbyLength)]
        public string releaseoutstandingby { get; set; }

        [StringLength(BillofladingConsts.MaxreleaselongstandingstatusLength, MinimumLength = BillofladingConsts.MinreleaselongstandingstatusLength)]
        public string releaselongstandingstatus { get; set; }

        [StringLength(BillofladingConsts.MaxreleaselongstandingreasonLength, MinimumLength = BillofladingConsts.MinreleaselongstandingreasonLength)]
        public string releaselongstandingreason { get; set; }

        [StringLength(BillofladingConsts.MaxreleaselongstandingbyLength, MinimumLength = BillofladingConsts.MinreleaselongstandingbyLength)]
        public string releaselongstandingby { get; set; }

        [StringLength(BillofladingConsts.MaxblnotypeLength, MinimumLength = BillofladingConsts.MinblnotypeLength)]
        public string blnotype { get; set; }

        [StringLength(BillofladingConsts.MaxblnosubmitstatusLength, MinimumLength = BillofladingConsts.MinblnosubmitstatusLength)]
        public string blnosubmitstatus { get; set; }

        [StringLength(BillofladingConsts.MaxblnosubmittedtoLength, MinimumLength = BillofladingConsts.MinblnosubmittedtoLength)]
        public string blnosubmittedto { get; set; }

        [StringLength(BillofladingConsts.MaxblnosubmittedbyLength, MinimumLength = BillofladingConsts.MinblnosubmittedbyLength)]
        public string blnosubmittedby { get; set; }

        [StringLength(BillofladingConsts.MaxblnosubmitrefLength, MinimumLength = BillofladingConsts.MinblnosubmitrefLength)]
        public string blnosubmitref { get; set; }

    }
}