using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Arch.Authorization.Users.Dto;

namespace Arch.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<PagedResultDto<UserLoginAttemptDto>> GetUserLoginAttempts(GetLoginAttemptsInput input);
    }
}
