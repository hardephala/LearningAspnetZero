using Arch.Billofladings.Dtos;

using Abp.Extensions;

namespace Arch.Web.Areas.App.Models.Billofladings
{
    public class CreateOrEditBillofladingModalViewModel
    {
        public CreateOrEditBillofladingDto Billoflading { get; set; }

        public bool IsEditMode => Billoflading.Id.HasValue;
    }
}