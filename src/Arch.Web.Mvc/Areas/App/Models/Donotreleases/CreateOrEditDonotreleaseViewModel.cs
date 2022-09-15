using Arch.Donotreleases.Dtos;

using Abp.Extensions;

namespace Arch.Web.Areas.App.Models.Donotreleases
{
    public class CreateOrEditDonotreleaseModalViewModel
    {
        public CreateOrEditDonotreleaseDto Donotrelease { get; set; }

        public bool IsEditMode => Donotrelease.Id.HasValue;
    }
}