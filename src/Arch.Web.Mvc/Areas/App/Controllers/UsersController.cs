﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arch.Authorization;
using Arch.Authorization.Permissions;
using Arch.Authorization.Permissions.Dto;
using Arch.Authorization.Roles;
using Arch.Authorization.Roles.Dto;
using Arch.Authorization.Users;
using Arch.Authorization.Users.Dto;
using Arch.Security;
using Arch.Web.Areas.App.Models.Roles;
using Arch.Web.Areas.App.Models.Users;
using Arch.Web.Controllers;

namespace Arch.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class UsersController : ArchControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly UserManager _userManager;
        private readonly IUserLoginAppService _userLoginAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly IPermissionAppService _permissionAppService;
        private readonly IPasswordComplexitySettingStore _passwordComplexitySettingStore;

        public UsersController(
            IUserAppService userAppService,
            UserManager userManager,
            IUserLoginAppService userLoginAppService,
            IRoleAppService roleAppService,
            IPermissionAppService permissionAppService,
            IPasswordComplexitySettingStore passwordComplexitySettingStore)
        {
            _userAppService = userAppService;
            _userManager = userManager;
            _userLoginAppService = userLoginAppService;
            _roleAppService = roleAppService;
            _permissionAppService = permissionAppService;
            _passwordComplexitySettingStore = passwordComplexitySettingStore;
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
        public async Task<ActionResult> Index()
        {
            var roles = new List<ComboboxItemDto>();

            if (await IsGrantedAsync(AppPermissions.Pages_Administration_Roles))
            {
                var getRolesOutput = await _roleAppService.GetRoles(new GetRolesInput());
                roles = getRolesOutput.Items.Select(r => new ComboboxItemDto(r.Id.ToString(), r.DisplayName)).ToList();
            }

            roles.Insert(0, new ComboboxItemDto("", L("FilterByRole")));

            var permissions = _permissionAppService.GetAllPermissions().Items.ToList();

            var model = new UsersViewModel
            {
                FilterText = Request.Query["filterText"],
                Roles = roles,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName)
                    .ToList(),
                OnlyLockedUsers = false
            };

            return View(model);
        }

        [AbpMvcAuthorize(
            AppPermissions.Pages_Administration_Users,
            AppPermissions.Pages_Administration_Users_Create,
            AppPermissions.Pages_Administration_Users_Edit
        )]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            var output = await _userAppService.GetUserForEdit(new NullableIdDto<long> {Id = id});
            var viewModel = ObjectMapper.Map<CreateOrEditUserModalViewModel>(output);
            viewModel.PasswordComplexitySetting = await _passwordComplexitySettingStore.GetSettingsAsync();

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(
            AppPermissions.Pages_Administration_Users,
            AppPermissions.Pages_Administration_Users_ChangePermissions
        )]
        public async Task<PartialViewResult> PermissionsModal(long id)
        {
            var output = await _userAppService.GetUserPermissionsForEdit(new EntityDto<long>(id));
            var viewModel = ObjectMapper.Map<UserPermissionsEditViewModel>(output);
            viewModel.User = await _userManager.GetUserByIdAsync(id);
            ;
            return PartialView("_PermissionsModal", viewModel);
        }

        public ActionResult LoginAttempts()
        {
            var loginResultTypes = Enum.GetNames(typeof(AbpLoginResultType))
                .Select(e => new ComboboxItemDto(e, L("AbpLoginResultType_" + e)))
                .ToList();

            loginResultTypes.Insert(0, new ComboboxItemDto("", L("All")));

            return View("LoginAttempts", new UserLoginAttemptsViewModel()
            {
                LoginAttemptResults = loginResultTypes
            });
        }
    }
}
