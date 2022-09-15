using System.Threading.Tasks;
using Abp.Application.Services;
using Arch.MultiTenancy.Payments.Dto;
using Arch.MultiTenancy.Payments.Stripe.Dto;

namespace Arch.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}