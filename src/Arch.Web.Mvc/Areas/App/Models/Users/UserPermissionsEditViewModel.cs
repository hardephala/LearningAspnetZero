using Abp.AutoMapper;
using Arch.Authorization.Users;
using Arch.Authorization.Users.Dto;
using Arch.Web.Areas.App.Models.Common;

namespace Arch.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}