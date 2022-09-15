using Abp.AspNetCore.Mvc.Authorization;
using Arch.Authorization;
using Arch.Storage;
using Abp.BackgroundJobs;

namespace Arch.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}