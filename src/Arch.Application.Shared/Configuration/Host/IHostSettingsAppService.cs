using System.Threading.Tasks;
using Abp.Application.Services;
using Arch.Configuration.Host.Dto;

namespace Arch.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
