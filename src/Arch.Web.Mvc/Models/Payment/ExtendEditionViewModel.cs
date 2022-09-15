using System.Collections.Generic;
using Arch.Editions.Dto;
using Arch.MultiTenancy.Payments;

namespace Arch.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}