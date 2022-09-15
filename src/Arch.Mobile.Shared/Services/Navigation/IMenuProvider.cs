using System.Collections.Generic;
using MvvmHelpers;
using Arch.Models.NavigationMenu;

namespace Arch.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}