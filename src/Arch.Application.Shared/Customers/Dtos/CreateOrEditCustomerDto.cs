using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Customers.Dtos
{
    public class CreateOrEditCustomerDto : EntityDto<long?>
    {

        [StringLength(CustomerConsts.MaxcustomerroleLength, MinimumLength = CustomerConsts.MincustomerroleLength)]
        public string customerrole { get; set; }

        [StringLength(CustomerConsts.MaxcustomercodeLength, MinimumLength = CustomerConsts.MincustomercodeLength)]
        public string customercode { get; set; }

        [StringLength(CustomerConsts.MaxcustomernameLength, MinimumLength = CustomerConsts.MincustomernameLength)]
        public string customername { get; set; }

        [StringLength(CustomerConsts.MaxcustomergroupcodeLength, MinimumLength = CustomerConsts.MincustomergroupcodeLength)]
        public string customergroupcode { get; set; }

        [StringLength(CustomerConsts.MaxcustomergroupnameLength, MinimumLength = CustomerConsts.MincustomergroupnameLength)]
        public string customergroupname { get; set; }

        [StringLength(CustomerConsts.MaxprimaryemailLength, MinimumLength = CustomerConsts.MinprimaryemailLength)]
        public string primaryemail { get; set; }

        [StringLength(CustomerConsts.MaxaltemailLength, MinimumLength = CustomerConsts.MinaltemailLength)]
        public string altemail { get; set; }

        [StringLength(CustomerConsts.MaxphonenumberLength, MinimumLength = CustomerConsts.MinphonenumberLength)]
        public string phonenumber { get; set; }

        [StringLength(CustomerConsts.MaxaccounttypeLength, MinimumLength = CustomerConsts.MinaccounttypeLength)]
        public string accounttype { get; set; }

        [StringLength(CustomerConsts.MaxlinkedcodeLength, MinimumLength = CustomerConsts.MinlinkedcodeLength)]
        public string linkedcode { get; set; }

        [StringLength(CustomerConsts.MaxpasswordLength, MinimumLength = CustomerConsts.MinpasswordLength)]
        public string password { get; set; }

        [StringLength(CustomerConsts.MaxstatusLength, MinimumLength = CustomerConsts.MinstatusLength)]
        public string status { get; set; }

        [StringLength(CustomerConsts.MaxnotesLength, MinimumLength = CustomerConsts.MinnotesLength)]
        public string notes { get; set; }

    }
}