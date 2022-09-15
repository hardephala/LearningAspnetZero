using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Arch.Authorization.Permissions.Dto;
using Arch.Web.Areas.App.Models.Common;

namespace Arch.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}