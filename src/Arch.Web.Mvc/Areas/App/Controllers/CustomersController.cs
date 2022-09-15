using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Customers;
using Arch.Web.Controllers;
using Arch.Authorization;
using Arch.Customers;
using Arch.Customers.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Arch.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Customers)]
    public class CustomersController : ArchControllerBase
    {
        private readonly ICustomersAppService _customersAppService;

        public CustomersController(ICustomersAppService customersAppService)
        {
            _customersAppService = customersAppService;

        }

        public ActionResult Index()
        {
            var model = new CustomersViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Customers_Create, AppPermissions.Pages_Administration_Customers_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetCustomerForEditOutput getCustomerForEditOutput;

            if (id.HasValue)
            {
                getCustomerForEditOutput = await _customersAppService.GetCustomerForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getCustomerForEditOutput = new GetCustomerForEditOutput
                {
                    Customer = new CreateOrEditCustomerDto()
                };
            }

            var viewModel = new CreateOrEditCustomerModalViewModel()
            {
                Customer = getCustomerForEditOutput.Customer,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewCustomerModal(long id)
        {
            var getCustomerForViewDto = await _customersAppService.GetCustomerForView(id);

            var model = new CustomerViewModel()
            {
                Customer = getCustomerForViewDto.Customer
            };

            return PartialView("_ViewCustomerModal", model);
        }

    }
}