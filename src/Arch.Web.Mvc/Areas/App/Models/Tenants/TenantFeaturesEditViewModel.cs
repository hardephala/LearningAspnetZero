using Abp.AutoMapper;
using Arch.MultiTenancy;
using Arch.MultiTenancy.Dto;
using Arch.Web.Areas.App.Models.Common;

namespace Arch.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}