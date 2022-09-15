using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Longstandings.Dtos
{
    public class CreateOrEditLongstandingDto : EntityDto<int?>
    {

        [StringLength(LongstandingConsts.MaxcustomercodeLength, MinimumLength = LongstandingConsts.MincustomercodeLength)]
        public string customercode { get; set; }

        [StringLength(LongstandingConsts.MaxblnoLength, MinimumLength = LongstandingConsts.MinblnoLength)]
        public string blno { get; set; }

        [StringLength(LongstandingConsts.MaxcontainernoLength, MinimumLength = LongstandingConsts.MincontainernoLength)]
        public string containerno { get; set; }

        [StringLength(LongstandingConsts.MaxcontainertypeLength, MinimumLength = LongstandingConsts.MincontainertypeLength)]
        public string containertype { get; set; }

        [StringLength(LongstandingConsts.MaxfreetextLength, MinimumLength = LongstandingConsts.MinfreetextLength)]
        public string freetext { get; set; }

        [StringLength(LongstandingConsts.MaxlocationLength, MinimumLength = LongstandingConsts.MinlocationLength)]
        public string location { get; set; }

        [StringLength(LongstandingConsts.MaxlastmoveLength, MinimumLength = LongstandingConsts.MinlastmoveLength)]
        public string lastmove { get; set; }

        [Range(LongstandingConsts.MindaysValue, LongstandingConsts.MaxdaysValue)]
        public int days { get; set; }

        [StringLength(LongstandingConsts.MaxstatusLength, MinimumLength = LongstandingConsts.MinstatusLength)]
        public string status { get; set; }

        [StringLength(LongstandingConsts.MaxreleasedbyLength, MinimumLength = LongstandingConsts.MinreleasedbyLength)]
        public string releasedby { get; set; }

        [StringLength(LongstandingConsts.MaxreleasedreasonLength, MinimumLength = LongstandingConsts.MinreleasedreasonLength)]
        public string releasedreason { get; set; }

        [StringLength(LongstandingConsts.MaxreleasecommentLength, MinimumLength = LongstandingConsts.MinreleasecommentLength)]
        public string releasecomment { get; set; }

        public DateTime validitydate { get; set; }

        [StringLength(LongstandingConsts.MaxshipoperatorLength, MinimumLength = LongstandingConsts.MinshipoperatorLength)]
        public string shipoperator { get; set; }

    }
}