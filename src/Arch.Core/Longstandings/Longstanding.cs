using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Arch.Longstandings
{
    [Table("longstandings")]
    public class Longstanding : AuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(LongstandingConsts.MaxcustomercodeLength, MinimumLength = LongstandingConsts.MincustomercodeLength)]
        public virtual string customercode { get; set; }

        [StringLength(LongstandingConsts.MaxblnoLength, MinimumLength = LongstandingConsts.MinblnoLength)]
        public virtual string blno { get; set; }

        [StringLength(LongstandingConsts.MaxcontainernoLength, MinimumLength = LongstandingConsts.MincontainernoLength)]
        public virtual string containerno { get; set; }

        [StringLength(LongstandingConsts.MaxcontainertypeLength, MinimumLength = LongstandingConsts.MincontainertypeLength)]
        public virtual string containertype { get; set; }

        [StringLength(LongstandingConsts.MaxfreetextLength, MinimumLength = LongstandingConsts.MinfreetextLength)]
        public virtual string freetext { get; set; }

        [StringLength(LongstandingConsts.MaxlocationLength, MinimumLength = LongstandingConsts.MinlocationLength)]
        public virtual string location { get; set; }

        [StringLength(LongstandingConsts.MaxlastmoveLength, MinimumLength = LongstandingConsts.MinlastmoveLength)]
        public virtual string lastmove { get; set; }

        [Range(LongstandingConsts.MindaysValue, LongstandingConsts.MaxdaysValue)]
        public virtual int days { get; set; }

        [StringLength(LongstandingConsts.MaxstatusLength, MinimumLength = LongstandingConsts.MinstatusLength)]
        public virtual string status { get; set; }

        [StringLength(LongstandingConsts.MaxreleasedbyLength, MinimumLength = LongstandingConsts.MinreleasedbyLength)]
        public virtual string releasedby { get; set; }

        [StringLength(LongstandingConsts.MaxreleasedreasonLength, MinimumLength = LongstandingConsts.MinreleasedreasonLength)]
        public virtual string releasedreason { get; set; }

        [StringLength(LongstandingConsts.MaxreleasecommentLength, MinimumLength = LongstandingConsts.MinreleasecommentLength)]
        public virtual string releasecomment { get; set; }

        public virtual DateTime validitydate { get; set; }

        [StringLength(LongstandingConsts.MaxshipoperatorLength, MinimumLength = LongstandingConsts.MinshipoperatorLength)]
        public virtual string shipoperator { get; set; }

    }
}