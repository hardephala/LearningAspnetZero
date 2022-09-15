using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Arch.Editions.Dto;

namespace Arch.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}