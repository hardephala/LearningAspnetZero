using System.Threading.Tasks;
using Abp.Application.Services;
using Arch.MultiTenancy.Payments.PayPal.Dto;

namespace Arch.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
