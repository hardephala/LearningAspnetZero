using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Arch.Cargoreleases
{
    [Table("Cargoreleases")]
    public class Cargorelease : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(CargoreleaseConsts.MaxpriorityLength, MinimumLength = CargoreleaseConsts.MinpriorityLength)]
        public virtual string priority { get; set; }

        [StringLength(CargoreleaseConsts.MaxblnoLength, MinimumLength = CargoreleaseConsts.MinblnoLength)]
        public virtual string blno { get; set; }

        public virtual DateTime invoicevalidity { get; set; }

        [StringLength(CargoreleaseConsts.MaxterminalLength, MinimumLength = CargoreleaseConsts.MinterminalLength)]
        public virtual string terminal { get; set; }

        [StringLength(CargoreleaseConsts.MaxdeliveryordernoLength, MinimumLength = CargoreleaseConsts.MindeliveryordernoLength)]
        public virtual string deliveryorderno { get; set; }

        [StringLength(CargoreleaseConsts.MaxcustomercodeLength, MinimumLength = CargoreleaseConsts.MincustomercodeLength)]
        public virtual string customercode { get; set; }

        [StringLength(CargoreleaseConsts.MaxagencycodeLength, MinimumLength = CargoreleaseConsts.MinagencycodeLength)]
        public virtual string agencycode { get; set; }

        [StringLength(CargoreleaseConsts.MaxagentcodeLength, MinimumLength = CargoreleaseConsts.MinagentcodeLength)]
        public virtual string agentcode { get; set; }

        [StringLength(CargoreleaseConsts.MaxentrybyrepcodeLength, MinimumLength = CargoreleaseConsts.MinentrybyrepcodeLength)]
        public virtual string entrybyrepcode { get; set; }

        [StringLength(CargoreleaseConsts.MaxentrymodeLength, MinimumLength = CargoreleaseConsts.MinentrymodeLength)]
        public virtual string entrymode { get; set; }

        public virtual DateTime entrydate { get; set; }

        [StringLength(CargoreleaseConsts.MaxapprovebyLength, MinimumLength = CargoreleaseConsts.MinapprovebyLength)]
        public virtual string approveby { get; set; }

        [StringLength(CargoreleaseConsts.MaxapprovecommentLength, MinimumLength = CargoreleaseConsts.MinapprovecommentLength)]
        public virtual string approvecomment { get; set; }

        public virtual DateTime approvedate { get; set; }

        [StringLength(CargoreleaseConsts.MaxupdatedbyLength, MinimumLength = CargoreleaseConsts.MinupdatedbyLength)]
        public virtual string updatedby { get; set; }

        [StringLength(CargoreleaseConsts.MaxupdatecommentLength, MinimumLength = CargoreleaseConsts.MinupdatecommentLength)]
        public virtual string updatecomment { get; set; }

        public virtual DateTime updatedate { get; set; }

        [StringLength(CargoreleaseConsts.MaxreleasebyLength, MinimumLength = CargoreleaseConsts.MinreleasebyLength)]
        public virtual string releaseby { get; set; }

        [StringLength(CargoreleaseConsts.MaxreleasestatusLength, MinimumLength = CargoreleaseConsts.MinreleasestatusLength)]
        public virtual string releasestatus { get; set; }

        [StringLength(CargoreleaseConsts.MaxreleasecommentLength, MinimumLength = CargoreleaseConsts.MinreleasecommentLength)]
        public virtual string releasecomment { get; set; }

        public virtual DateTime releasedate { get; set; }

        [StringLength(CargoreleaseConsts.MaxstatusLength, MinimumLength = CargoreleaseConsts.MinstatusLength)]
        public virtual string status { get; set; }

        [StringLength(CargoreleaseConsts.MaxipaddrLength, MinimumLength = CargoreleaseConsts.MinipaddrLength)]
        public virtual string ipaddr { get; set; }

    }
}