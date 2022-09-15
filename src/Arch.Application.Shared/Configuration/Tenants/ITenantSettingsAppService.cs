using System.Threading.Tasks;
using Abp.Application.Services;
using Arch.Configuration.Tenants.Dto;

namespace Arch.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
