using System.Threading.Tasks;
using Arch.Authorization.Users;

namespace Arch.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
