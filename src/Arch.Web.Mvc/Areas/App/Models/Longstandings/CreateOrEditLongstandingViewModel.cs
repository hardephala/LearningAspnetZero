using Arch.Longstandings.Dtos;

using Abp.Extensions;

namespace Arch.Web.Areas.App.Models.Longstandings
{
    public class CreateOrEditLongstandingModalViewModel
    {
        public CreateOrEditLongstandingDto Longstanding { get; set; }

        public bool IsEditMode => Longstanding.Id.HasValue;
    }
}