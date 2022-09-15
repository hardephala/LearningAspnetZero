using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Longstandings;
using Arch.Web.Controllers;
using Arch.Authorization;
using Arch.Longstandings;
using Arch.Longstandings.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Arch.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Longstandings)]
    public class LongstandingsController : ArchControllerBase
    {
        private readonly ILongstandingsAppService _longstandingsAppService;

        public LongstandingsController(ILongstandingsAppService longstandingsAppService)
        {
            _longstandingsAppService = longstandingsAppService;

        }

        public ActionResult Index()
        {
            var model = new LongstandingsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Longstandings_Create, AppPermissions.Pages_Longstandings_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetLongstandingForEditOutput getLongstandingForEditOutput;

            if (id.HasValue)
            {
                getLongstandingForEditOutput = await _longstandingsAppService.GetLongstandingForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getLongstandingForEditOutput = new GetLongstandingForEditOutput
                {
                    Longstanding = new CreateOrEditLongstandingDto()
                };
                getLongstandingForEditOutput.Longstanding.validitydate = DateTime.Now;
            }

            var viewModel = new CreateOrEditLongstandingModalViewModel()
            {
                Longstanding = getLongstandingForEditOutput.Longstanding,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewLongstandingModal(int id)
        {
            var getLongstandingForViewDto = await _longstandingsAppService.GetLongstandingForView(id);

            var model = new LongstandingViewModel()
            {
                Longstanding = getLongstandingForViewDto.Longstanding
            };

            return PartialView("_ViewLongstandingModal", model);
        }

    }
}