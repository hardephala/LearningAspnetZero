using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Donotreleases;
using Arch.Web.Controllers;
using Arch.Authorization;
using Arch.Donotreleases;
using Arch.Donotreleases.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Arch.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Donotreleases)]
    public class DonotreleasesController : ArchControllerBase
    {
        private readonly IDonotreleasesAppService _donotreleasesAppService;

        public DonotreleasesController(IDonotreleasesAppService donotreleasesAppService)
        {
            _donotreleasesAppService = donotreleasesAppService;

        }

        public ActionResult Index()
        {
            var model = new DonotreleasesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Donotreleases_Create, AppPermissions.Pages_Donotreleases_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetDonotreleaseForEditOutput getDonotreleaseForEditOutput;

            if (id.HasValue)
            {
                getDonotreleaseForEditOutput = await _donotreleasesAppService.GetDonotreleaseForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getDonotreleaseForEditOutput = new GetDonotreleaseForEditOutput
                {
                    Donotrelease = new CreateOrEditDonotreleaseDto()
                };
                getDonotreleaseForEditOutput.Donotrelease.blockeddate = DateTime.Now;
            }

            var viewModel = new CreateOrEditDonotreleaseModalViewModel()
            {
                Donotrelease = getDonotreleaseForEditOutput.Donotrelease,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewDonotreleaseModal(int id)
        {
            var getDonotreleaseForViewDto = await _donotreleasesAppService.GetDonotreleaseForView(id);

            var model = new DonotreleaseViewModel()
            {
                Donotrelease = getDonotreleaseForViewDto.Donotrelease
            };

            return PartialView("_ViewDonotreleaseModal", model);
        }

    }
}