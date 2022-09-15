using Abp.AutoMapper;
using Arch.MultiTenancy.Dto;

namespace Arch.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
