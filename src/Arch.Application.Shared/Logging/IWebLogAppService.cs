using Abp.Application.Services;
using Arch.Dto;
using Arch.Logging.Dto;

namespace Arch.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
