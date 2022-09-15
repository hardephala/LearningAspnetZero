using System.Threading.Tasks;
using Abp.Application.Services;
using Arch.Editions.Dto;
using Arch.MultiTenancy.Dto;

namespace Arch.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}