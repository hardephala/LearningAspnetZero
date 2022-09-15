using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Donotreleases.Dtos
{
    public class CreateOrEditDonotreleaseDto : EntityDto<int?>
    {

        [StringLength(DonotreleaseConsts.MaxblnoLength, MinimumLength = DonotreleaseConsts.MinblnoLength)]
        public string blno { get; set; }

        [StringLength(DonotreleaseConsts.MaxstatusLength, MinimumLength = DonotreleaseConsts.MinstatusLength)]
        public string status { get; set; }

        [StringLength(DonotreleaseConsts.MaxreleasedbyLength, MinimumLength = DonotreleaseConsts.MinreleasedbyLength)]
        public string releasedby { get; set; }

        [StringLength(DonotreleaseConsts.MaxreleasecommentLength, MinimumLength = DonotreleaseConsts.MinreleasecommentLength)]
        public string releasecomment { get; set; }

        [StringLength(DonotreleaseConsts.MaxblockedbyLength, MinimumLength = DonotreleaseConsts.MinblockedbyLength)]
        public string blockedby { get; set; }

        [StringLength(DonotreleaseConsts.MaxblockedcommentLength, MinimumLength = DonotreleaseConsts.MinblockedcommentLength)]
        public string blockedcomment { get; set; }

        public DateTime blockeddate { get; set; }

        [StringLength(DonotreleaseConsts.MaxblockedreferenceLength, MinimumLength = DonotreleaseConsts.MinblockedreferenceLength)]
        public string blockedreference { get; set; }

        [StringLength(DonotreleaseConsts.MaxblcommentLength, MinimumLength = DonotreleaseConsts.MinblcommentLength)]
        public string blcomment { get; set; }

    }
}