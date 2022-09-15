using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Billofladings;
using Arch.Web.Controllers;
using Arch.Authorization;
using Arch.Billofladings;
using Arch.Billofladings.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Arch.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Billofladings)]
    public class BillofladingsController : ArchControllerBase
    {
        private readonly IBillofladingsAppService _billofladingsAppService;

        public BillofladingsController(IBillofladingsAppService billofladingsAppService)
        {
            _billofladingsAppService = billofladingsAppService;

        }

        public ActionResult Index()
        {
            var model = new BillofladingsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Billofladings_Create, AppPermissions.Pages_Billofladings_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetBillofladingForEditOutput getBillofladingForEditOutput;

            if (id.HasValue)
            {
                getBillofladingForEditOutput = await _billofladingsAppService.GetBillofladingForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getBillofladingForEditOutput = new GetBillofladingForEditOutput
                {
                    Billoflading = new CreateOrEditBillofladingDto()
                };
                getBillofladingForEditOutput.Billoflading.finalvesseleta = DateTime.Now;
            }

            var viewModel = new CreateOrEditBillofladingModalViewModel()
            {
                Billoflading = getBillofladingForEditOutput.Billoflading,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewBillofladingModal(long id)
        {
            var getBillofladingForViewDto = await _billofladingsAppService.GetBillofladingForView(id);

            var model = new BillofladingViewModel()
            {
                Billoflading = getBillofladingForViewDto.Billoflading
            };

            return PartialView("_ViewBillofladingModal", model);
        }

    }
}