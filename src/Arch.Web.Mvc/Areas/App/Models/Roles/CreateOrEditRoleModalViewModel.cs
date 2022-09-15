using Abp.AutoMapper;
using Arch.Authorization.Roles.Dto;
using Arch.Web.Areas.App.Models.Common;

namespace Arch.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}