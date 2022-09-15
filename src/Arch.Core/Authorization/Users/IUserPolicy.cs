using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Arch.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
