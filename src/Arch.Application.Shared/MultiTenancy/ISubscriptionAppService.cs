using System.Threading.Tasks;
using Abp.Application.Services;

namespace Arch.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
