using Abp.Authorization;
using Arch.Authorization.Roles;
using Arch.Authorization.Users;

namespace Arch.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
