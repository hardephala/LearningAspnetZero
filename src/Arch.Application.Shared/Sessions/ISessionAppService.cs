using System.Threading.Tasks;
using Abp.Application.Services;
using Arch.Sessions.Dto;

namespace Arch.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
