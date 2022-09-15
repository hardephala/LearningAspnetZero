using System;
using Abp.Application.Services.Dto;

namespace Arch.Cargoreleases.Dtos
{
    public class CargoreleaseDto : EntityDto<long>
    {
        public string priority { get; set; }

        public string blno { get; set; }

        public DateTime invoicevalidity { get; set; }

        public string terminal { get; set; }

        public string deliveryorderno { get; set; }

        public string customercode { get; set; }

        public string agencycode { get; set; }

        public string agentcode { get; set; }

        public string entrybyrepcode { get; set; }

        public string entrymode { get; set; }

        public DateTime entrydate { get; set; }

        public string approveby { get; set; }

        public string approvecomment { get; set; }

        public DateTime approvedate { get; set; }

        public string updatedby { get; set; }

        public string updatecomment { get; set; }

        public DateTime updatedate { get; set; }

        public string releaseby { get; set; }

        public string releasestatus { get; set; }

        public string releasecomment { get; set; }

        public DateTime releasedate { get; set; }

        public string status { get; set; }

        public string ipaddr { get; set; }

    }
}