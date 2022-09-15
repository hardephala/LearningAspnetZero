using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Arch.Billofladings
{
    [Table("Billofladings")]
    public class Billoflading : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(BillofladingConsts.MaxshipmentnoLength, MinimumLength = BillofladingConsts.MinshipmentnoLength)]
        public virtual string shipmentno { get; set; }

        [StringLength(BillofladingConsts.MaxblnoLength, MinimumLength = BillofladingConsts.MinblnoLength)]
        public virtual string blno { get; set; }

        [StringLength(BillofladingConsts.MaxequipmentnoLength, MinimumLength = BillofladingConsts.MinequipmentnoLength)]
        public virtual string equipmentno { get; set; }

        [StringLength(BillofladingConsts.MaxequipmenttypeLength, MinimumLength = BillofladingConsts.MinequipmenttypeLength)]
        public virtual string equipmenttype { get; set; }

        [StringLength(BillofladingConsts.MaxequipmentsizeLength, MinimumLength = BillofladingConsts.MinequipmentsizeLength)]
        public virtual string equipmentsize { get; set; }

        [StringLength(BillofladingConsts.MaxshipperownedLength, MinimumLength = BillofladingConsts.MinshipperownedLength)]
        public virtual string shipperowned { get; set; }

        [StringLength(BillofladingConsts.MaxshipoperatorLength, MinimumLength = BillofladingConsts.MinshipoperatorLength)]
        public virtual string shipoperator { get; set; }

        [StringLength(BillofladingConsts.MaxservicecontractLength, MinimumLength = BillofladingConsts.MinservicecontractLength)]
        public virtual string servicecontract { get; set; }

        [StringLength(BillofladingConsts.MaxspotbookingLength, MinimumLength = BillofladingConsts.MinspotbookingLength)]
        public virtual string spotbooking { get; set; }

        [StringLength(BillofladingConsts.MaxconsigneecodeLength, MinimumLength = BillofladingConsts.MinconsigneecodeLength)]
        public virtual string consigneecode { get; set; }

        [StringLength(BillofladingConsts.MaxdischargeportcodeLength, MinimumLength = BillofladingConsts.MindischargeportcodeLength)]
        public virtual string dischargeportcode { get; set; }

        public virtual string dischargeportname { get; set; }

        [StringLength(BillofladingConsts.MaxplaceofdeliverycodeLength, MinimumLength = BillofladingConsts.MinplaceofdeliverycodeLength)]
        public virtual string placeofdeliverycode { get; set; }

        [StringLength(BillofladingConsts.MaxplaceofdeliverynameLength, MinimumLength = BillofladingConsts.MinplaceofdeliverynameLength)]
        public virtual string placeofdeliveryname { get; set; }

        [StringLength(BillofladingConsts.MaxfinalvesselcodeLength, MinimumLength = BillofladingConsts.MinfinalvesselcodeLength)]
        public virtual string finalvesselcode { get; set; }

        [StringLength(BillofladingConsts.MaxfinalvesselnameLength, MinimumLength = BillofladingConsts.MinfinalvesselnameLength)]
        public virtual string finalvesselname { get; set; }

        [StringLength(BillofladingConsts.MaxfinalvesselvoyageLength, MinimumLength = BillofladingConsts.MinfinalvesselvoyageLength)]
        public virtual string finalvesselvoyage { get; set; }

        public virtual DateTime finalvesseleta { get; set; }

        [StringLength(BillofladingConsts.MaxpartpartstatusLength, MinimumLength = BillofladingConsts.MinpartpartstatusLength)]
        public virtual string partpartstatus { get; set; }

        [StringLength(BillofladingConsts.MaxpartpartrefLength, MinimumLength = BillofladingConsts.MinpartpartrefLength)]
        public virtual string partpartref { get; set; }

        [StringLength(BillofladingConsts.MaxdepositpayableLength, MinimumLength = BillofladingConsts.MindepositpayableLength)]
        public virtual string depositpayable { get; set; }

        [Range(BillofladingConsts.MindepositdueamountValue, BillofladingConsts.MaxdepositdueamountValue)]
        public virtual int depositdueamount { get; set; }

        [StringLength(BillofladingConsts.MaxdepositwaivedamountLength, MinimumLength = BillofladingConsts.MindepositwaivedamountLength)]
        public virtual string depositwaivedamount { get; set; }

        [StringLength(BillofladingConsts.MaxdepositwaivedreasonLength, MinimumLength = BillofladingConsts.MindepositwaivedreasonLength)]
        public virtual string depositwaivedreason { get; set; }

        [StringLength(BillofladingConsts.MaxdepositwaivedbyLength, MinimumLength = BillofladingConsts.MindepositwaivedbyLength)]
        public virtual string depositwaivedby { get; set; }

        [StringLength(BillofladingConsts.MaxdepositpaymentstatusLength, MinimumLength = BillofladingConsts.MindepositpaymentstatusLength)]
        public virtual string depositpaymentstatus { get; set; }

        [StringLength(BillofladingConsts.MaxreleaseoutstandingstatusLength, MinimumLength = BillofladingConsts.MinreleaseoutstandingstatusLength)]
        public virtual string releaseoutstandingstatus { get; set; }

        [StringLength(BillofladingConsts.MaxreleaseoutstandingreasonLength, MinimumLength = BillofladingConsts.MinreleaseoutstandingreasonLength)]
        public virtual string releaseoutstandingreason { get; set; }

        [StringLength(BillofladingConsts.MaxreleaseoutstandingbyLength, MinimumLength = BillofladingConsts.MinreleaseoutstandingbyLength)]
        public virtual string releaseoutstandingby { get; set; }

        [StringLength(BillofladingConsts.MaxreleaselongstandingstatusLength, MinimumLength = BillofladingConsts.MinreleaselongstandingstatusLength)]
        public virtual string releaselongstandingstatus { get; set; }

        [StringLength(BillofladingConsts.MaxreleaselongstandingreasonLength, MinimumLength = BillofladingConsts.MinreleaselongstandingreasonLength)]
        public virtual string releaselongstandingreason { get; set; }

        [StringLength(BillofladingConsts.MaxreleaselongstandingbyLength, MinimumLength = BillofladingConsts.MinreleaselongstandingbyLength)]
        public virtual string releaselongstandingby { get; set; }

        [StringLength(BillofladingConsts.MaxblnotypeLength, MinimumLength = BillofladingConsts.MinblnotypeLength)]
        public virtual string blnotype { get; set; }

        [StringLength(BillofladingConsts.MaxblnosubmitstatusLength, MinimumLength = BillofladingConsts.MinblnosubmitstatusLength)]
        public virtual string blnosubmitstatus { get; set; }

        [StringLength(BillofladingConsts.MaxblnosubmittedtoLength, MinimumLength = BillofladingConsts.MinblnosubmittedtoLength)]
        public virtual string blnosubmittedto { get; set; }

        [StringLength(BillofladingConsts.MaxblnosubmittedbyLength, MinimumLength = BillofladingConsts.MinblnosubmittedbyLength)]
        public virtual string blnosubmittedby { get; set; }

        [StringLength(BillofladingConsts.MaxblnosubmitrefLength, MinimumLength = BillofladingConsts.MinblnosubmitrefLength)]
        public virtual string blnosubmitref { get; set; }

    }
}