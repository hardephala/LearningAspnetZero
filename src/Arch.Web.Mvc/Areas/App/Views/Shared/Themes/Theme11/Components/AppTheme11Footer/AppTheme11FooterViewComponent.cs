using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Layout;
using Arch.Web.Session;
using Arch.Web.Views;

namespace Arch.Web.Areas.App.Views.Shared.Themes.Theme11.Components.AppTheme11Footer
{
    public class AppTheme11FooterViewComponent : ArchViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme11FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
