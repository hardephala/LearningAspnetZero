﻿namespace Arch.Services.Permission
{
    public interface IPermissionService
    {
        bool HasPermission(string key);
    }
}