using System.Threading.Tasks;
using Abp.Webhooks;

namespace Arch.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
