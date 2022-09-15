using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Arch.Donotreleases
{
    [Table("donotreleases")]
    public class Donotrelease : AuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(DonotreleaseConsts.MaxblnoLength, MinimumLength = DonotreleaseConsts.MinblnoLength)]
        public virtual string blno { get; set; }

        [StringLength(DonotreleaseConsts.MaxstatusLength, MinimumLength = DonotreleaseConsts.MinstatusLength)]
        public virtual string status { get; set; }

        [StringLength(DonotreleaseConsts.MaxreleasedbyLength, MinimumLength = DonotreleaseConsts.MinreleasedbyLength)]
        public virtual string releasedby { get; set; }

        [StringLength(DonotreleaseConsts.MaxreleasecommentLength, MinimumLength = DonotreleaseConsts.MinreleasecommentLength)]
        public virtual string releasecomment { get; set; }

        [StringLength(DonotreleaseConsts.MaxblockedbyLength, MinimumLength = DonotreleaseConsts.MinblockedbyLength)]
        public virtual string blockedby { get; set; }

        [StringLength(DonotreleaseConsts.MaxblockedcommentLength, MinimumLength = DonotreleaseConsts.MinblockedcommentLength)]
        public virtual string blockedcomment { get; set; }

        public virtual DateTime blockeddate { get; set; }

        [StringLength(DonotreleaseConsts.MaxblockedreferenceLength, MinimumLength = DonotreleaseConsts.MinblockedreferenceLength)]
        public virtual string blockedreference { get; set; }

        [StringLength(DonotreleaseConsts.MaxblcommentLength, MinimumLength = DonotreleaseConsts.MinblcommentLength)]
        public virtual string blcomment { get; set; }

    }
}