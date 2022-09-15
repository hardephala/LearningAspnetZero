using System;
using Abp.Application.Services.Dto;

namespace Arch.Donotreleases.Dtos
{
    public class DonotreleaseDto : EntityDto
    {
        public string blno { get; set; }

        public string status { get; set; }

        public string releasedby { get; set; }

        public string releasecomment { get; set; }

        public string blockedby { get; set; }

        public string blockedcomment { get; set; }

        public DateTime blockeddate { get; set; }

        public string blockedreference { get; set; }

        public string blcomment { get; set; }

    }
}