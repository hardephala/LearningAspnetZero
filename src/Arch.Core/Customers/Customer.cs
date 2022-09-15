using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Arch.Customers
{
    [Table("customers")]
    [Audited]
    public class Customer : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(CustomerConsts.MaxcustomerroleLength, MinimumLength = CustomerConsts.MincustomerroleLength)]
        public virtual string customerrole { get; set; }

        [StringLength(CustomerConsts.MaxcustomercodeLength, MinimumLength = CustomerConsts.MincustomercodeLength)]
        public virtual string customercode { get; set; }

        [StringLength(CustomerConsts.MaxcustomernameLength, MinimumLength = CustomerConsts.MincustomernameLength)]
        public virtual string customername { get; set; }

        [StringLength(CustomerConsts.MaxcustomergroupcodeLength, MinimumLength = CustomerConsts.MincustomergroupcodeLength)]
        public virtual string customergroupcode { get; set; }

        [StringLength(CustomerConsts.MaxcustomergroupnameLength, MinimumLength = CustomerConsts.MincustomergroupnameLength)]
        public virtual string customergroupname { get; set; }

        [StringLength(CustomerConsts.MaxprimaryemailLength, MinimumLength = CustomerConsts.MinprimaryemailLength)]
        public virtual string primaryemail { get; set; }

        [StringLength(CustomerConsts.MaxaltemailLength, MinimumLength = CustomerConsts.MinaltemailLength)]
        public virtual string altemail { get; set; }

        [StringLength(CustomerConsts.MaxphonenumberLength, MinimumLength = CustomerConsts.MinphonenumberLength)]
        public virtual string phonenumber { get; set; }

        [StringLength(CustomerConsts.MaxaccounttypeLength, MinimumLength = CustomerConsts.MinaccounttypeLength)]
        public virtual string accounttype { get; set; }

        [StringLength(CustomerConsts.MaxlinkedcodeLength, MinimumLength = CustomerConsts.MinlinkedcodeLength)]
        public virtual string linkedcode { get; set; }

        [StringLength(CustomerConsts.MaxpasswordLength, MinimumLength = CustomerConsts.MinpasswordLength)]
        public virtual string password { get; set; }

        [StringLength(CustomerConsts.MaxstatusLength, MinimumLength = CustomerConsts.MinstatusLength)]
        public virtual string status { get; set; }

        [StringLength(CustomerConsts.MaxnotesLength, MinimumLength = CustomerConsts.MinnotesLength)]
        public virtual string notes { get; set; }

    }
}