using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Invoicedatas;
using Arch.Web.Controllers;
using Arch.Authorization;
using Arch.Invoicedatas;
using Arch.Invoicedatas.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Arch.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Invoicedatas)]
    public class InvoicedatasController : ArchControllerBase
    {
        private readonly IInvoicedatasAppService _invoicedatasAppService;

        public InvoicedatasController(IInvoicedatasAppService invoicedatasAppService)
        {
            _invoicedatasAppService = invoicedatasAppService;

        }

        public ActionResult Index()
        {
            var model = new InvoicedatasViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Invoicedatas_Create, AppPermissions.Pages_Invoicedatas_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetInvoicedataForEditOutput getInvoicedataForEditOutput;

            if (id.HasValue)
            {
                getInvoicedataForEditOutput = await _invoicedatasAppService.GetInvoicedataForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getInvoicedataForEditOutput = new GetInvoicedataForEditOutput
                {
                    Invoicedata = new CreateOrEditInvoicedataDto()
                };
                getInvoicedataForEditOutput.Invoicedata.validitydate = DateTime.Now;
                getInvoicedataForEditOutput.Invoicedata.invpaiddate = DateTime.Now;
                getInvoicedataForEditOutput.Invoicedata.datewaived = DateTime.Now;
                getInvoicedataForEditOutput.Invoicedata.CreationTime = DateTime.Now;
                getInvoicedataForEditOutput.Invoicedata.LastModificationTime = DateTime.Now;
            }

            var viewModel = new CreateOrEditInvoicedataModalViewModel()
            {
                Invoicedata = getInvoicedataForEditOutput.Invoicedata,
                Billofladingblno = getInvoicedataForEditOutput.Billofladingblno,
                InvoicedataBillofladingList = await _invoicedatasAppService.GetAllBillofladingForTableDropdown(),

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewInvoicedataModal(int id)
        {
            var getInvoicedataForViewDto = await _invoicedatasAppService.GetInvoicedataForView(id);

            var model = new InvoicedataViewModel()
            {
                Invoicedata = getInvoicedataForViewDto.Invoicedata
                ,
                Billofladingblno = getInvoicedataForViewDto.Billofladingblno

            };

            return PartialView("_ViewInvoicedataModal", model);
        }

    }
}