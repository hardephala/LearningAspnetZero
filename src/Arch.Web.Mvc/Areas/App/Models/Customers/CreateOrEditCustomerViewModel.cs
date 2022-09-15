using Arch.Customers.Dtos;

using Abp.Extensions;

namespace Arch.Web.Areas.App.Models.Customers
{
    public class CreateOrEditCustomerModalViewModel
    {
        public CreateOrEditCustomerDto Customer { get; set; }

        public bool IsEditMode => Customer.Id.HasValue;
    }
}