using Arch.Cargoreleases.Dtos;

using Abp.Extensions;

namespace Arch.Web.Areas.App.Models.Cargoreleases
{
    public class CreateOrEditCargoreleaseModalViewModel
    {
        public CreateOrEditCargoreleaseDto Cargorelease { get; set; }

        public bool IsEditMode => Cargorelease.Id.HasValue;
    }
}