using System.Threading.Tasks;
using Abp.Application.Services;
using Arch.Install.Dto;

namespace Arch.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}