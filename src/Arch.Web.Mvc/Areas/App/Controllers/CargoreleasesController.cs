using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Cargoreleases;
using Arch.Web.Controllers;
using Arch.Authorization;
using Arch.Cargoreleases;
using Arch.Cargoreleases.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Arch.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Cargoreleases)]
    public class CargoreleasesController : ArchControllerBase
    {
        private readonly ICargoreleasesAppService _cargoreleasesAppService;

        public CargoreleasesController(ICargoreleasesAppService cargoreleasesAppService)
        {
            _cargoreleasesAppService = cargoreleasesAppService;

        }

        public ActionResult Index()
        {
            var model = new CargoreleasesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Cargoreleases_Create, AppPermissions.Pages_Cargoreleases_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetCargoreleaseForEditOutput getCargoreleaseForEditOutput;

            if (id.HasValue)
            {
                getCargoreleaseForEditOutput = await _cargoreleasesAppService.GetCargoreleaseForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getCargoreleaseForEditOutput = new GetCargoreleaseForEditOutput
                {
                    Cargorelease = new CreateOrEditCargoreleaseDto()
                };
                getCargoreleaseForEditOutput.Cargorelease.invoicevalidity = DateTime.Now;
                getCargoreleaseForEditOutput.Cargorelease.entrydate = DateTime.Now;
                getCargoreleaseForEditOutput.Cargorelease.approvedate = DateTime.Now;
                getCargoreleaseForEditOutput.Cargorelease.updatedate = DateTime.Now;
                getCargoreleaseForEditOutput.Cargorelease.releasedate = DateTime.Now;
            }

            var viewModel = new CreateOrEditCargoreleaseModalViewModel()
            {
                Cargorelease = getCargoreleaseForEditOutput.Cargorelease,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewCargoreleaseModal(long id)
        {
            var getCargoreleaseForViewDto = await _cargoreleasesAppService.GetCargoreleaseForView(id);

            var model = new CargoreleaseViewModel()
            {
                Cargorelease = getCargoreleaseForViewDto.Cargorelease
            };

            return PartialView("_ViewCargoreleaseModal", model);
        }

    }
}