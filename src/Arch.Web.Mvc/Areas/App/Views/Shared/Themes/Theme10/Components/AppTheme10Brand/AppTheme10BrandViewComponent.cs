using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Layout;
using Arch.Web.Session;
using Arch.Web.Views;

namespace Arch.Web.Areas.App.Views.Shared.Themes.Theme10.Components.AppTheme10Brand
{
    public class AppTheme10BrandViewComponent : ArchViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme10BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
