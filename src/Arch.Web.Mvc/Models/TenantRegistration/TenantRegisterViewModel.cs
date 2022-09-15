using Arch.Editions;
using Arch.Editions.Dto;
using Arch.MultiTenancy.Payments;
using Arch.Security;
using Arch.MultiTenancy.Payments.Dto;

namespace Arch.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
