using System.Collections.Generic;
using Arch.Editions;
using Arch.Editions.Dto;
using Arch.MultiTenancy.Payments;
using Arch.MultiTenancy.Payments.Dto;

namespace Arch.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
