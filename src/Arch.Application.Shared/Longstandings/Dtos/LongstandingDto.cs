using System;
using Abp.Application.Services.Dto;

namespace Arch.Longstandings.Dtos
{
    public class LongstandingDto : EntityDto
    {
        public string customercode { get; set; }

        public string blno { get; set; }

        public string containerno { get; set; }

        public string containertype { get; set; }

        public string freetext { get; set; }

        public string location { get; set; }

        public string lastmove { get; set; }

        public int days { get; set; }

        public string status { get; set; }

        public string releasedby { get; set; }

        public string releasedreason { get; set; }

        public string releasecomment { get; set; }

        public DateTime validitydate { get; set; }

        public string shipoperator { get; set; }

    }
}