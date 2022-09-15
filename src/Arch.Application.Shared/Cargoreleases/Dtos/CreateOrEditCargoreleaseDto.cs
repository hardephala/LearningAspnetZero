using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Cargoreleases.Dtos
{
    public class CreateOrEditCargoreleaseDto : EntityDto<long?>
    {

        [StringLength(CargoreleaseConsts.MaxpriorityLength, MinimumLength = CargoreleaseConsts.MinpriorityLength)]
        public string priority { get; set; }

        [StringLength(CargoreleaseConsts.MaxblnoLength, MinimumLength = CargoreleaseConsts.MinblnoLength)]
        public string blno { get; set; }

        public DateTime invoicevalidity { get; set; }

        [StringLength(CargoreleaseConsts.MaxterminalLength, MinimumLength = CargoreleaseConsts.MinterminalLength)]
        public string terminal { get; set; }

        [StringLength(CargoreleaseConsts.MaxdeliveryordernoLength, MinimumLength = CargoreleaseConsts.MindeliveryordernoLength)]
        public string deliveryorderno { get; set; }

        [StringLength(CargoreleaseConsts.MaxcustomercodeLength, MinimumLength = CargoreleaseConsts.MincustomercodeLength)]
        public string customercode { get; set; }

        [StringLength(CargoreleaseConsts.MaxagencycodeLength, MinimumLength = CargoreleaseConsts.MinagencycodeLength)]
        public string agencycode { get; set; }

        [StringLength(CargoreleaseConsts.MaxagentcodeLength, MinimumLength = CargoreleaseConsts.MinagentcodeLength)]
        public string agentcode { get; set; }

        [StringLength(CargoreleaseConsts.MaxentrybyrepcodeLength, MinimumLength = CargoreleaseConsts.MinentrybyrepcodeLength)]
        public string entrybyrepcode { get; set; }

        [StringLength(CargoreleaseConsts.MaxentrymodeLength, MinimumLength = CargoreleaseConsts.MinentrymodeLength)]
        public string entrymode { get; set; }

        public DateTime entrydate { get; set; }

        [StringLength(CargoreleaseConsts.MaxapprovebyLength, MinimumLength = CargoreleaseConsts.MinapprovebyLength)]
        public string approveby { get; set; }

        [StringLength(CargoreleaseConsts.MaxapprovecommentLength, MinimumLength = CargoreleaseConsts.MinapprovecommentLength)]
        public string approvecomment { get; set; }

        public DateTime approvedate { get; set; }

        [StringLength(CargoreleaseConsts.MaxupdatedbyLength, MinimumLength = CargoreleaseConsts.MinupdatedbyLength)]
        public string updatedby { get; set; }

        [StringLength(CargoreleaseConsts.MaxupdatecommentLength, MinimumLength = CargoreleaseConsts.MinupdatecommentLength)]
        public string updatecomment { get; set; }

        public DateTime updatedate { get; set; }

        [StringLength(CargoreleaseConsts.MaxreleasebyLength, MinimumLength = CargoreleaseConsts.MinreleasebyLength)]
        public string releaseby { get; set; }

        [StringLength(CargoreleaseConsts.MaxreleasestatusLength, MinimumLength = CargoreleaseConsts.MinreleasestatusLength)]
        public string releasestatus { get; set; }

        [StringLength(CargoreleaseConsts.MaxreleasecommentLength, MinimumLength = CargoreleaseConsts.MinreleasecommentLength)]
        public string releasecomment { get; set; }

        public DateTime releasedate { get; set; }

        [StringLength(CargoreleaseConsts.MaxstatusLength, MinimumLength = CargoreleaseConsts.MinstatusLength)]
        public string status { get; set; }

        [StringLength(CargoreleaseConsts.MaxipaddrLength, MinimumLength = CargoreleaseConsts.MinipaddrLength)]
        public string ipaddr { get; set; }

    }
}