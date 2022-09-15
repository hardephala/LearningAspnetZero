using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Arch.Authorization.Permissions.Dto;

namespace Arch.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
