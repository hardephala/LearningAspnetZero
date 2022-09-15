using Arch.MultiTenancy.Payments.Stripe;

namespace Arch.Web.Controllers
{
    public class StripeController : StripeControllerBase
    {
        public StripeController(
            StripeGatewayManager stripeGatewayManager,
            StripePaymentGatewayConfiguration stripeConfiguration,
            IStripePaymentAppService stripePaymentAppService) 
            : base(stripeGatewayManager, stripeConfiguration, stripePaymentAppService)
        {
        }
    }
}
