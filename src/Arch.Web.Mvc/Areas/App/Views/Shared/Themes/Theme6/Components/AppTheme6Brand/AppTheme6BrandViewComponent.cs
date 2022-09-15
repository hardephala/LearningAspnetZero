using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Layout;
using Arch.Web.Session;
using Arch.Web.Views;

namespace Arch.Web.Areas.App.Views.Shared.Themes.Theme6.Components.AppTheme6Brand
{
    public class AppTheme6BrandViewComponent : ArchViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme6BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync(string skin = "dark-sm")
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
            };

            ViewBag.BrandLogoSkin = skin;

            return View(headerModel);
        }
    }
}
