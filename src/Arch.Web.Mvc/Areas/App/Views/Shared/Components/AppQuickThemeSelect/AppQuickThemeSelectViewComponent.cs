using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.Layout;
using Arch.Web.Views;

namespace Arch.Web.Areas.App.Views.Shared.Components.
    AppQuickThemeSelect
{
    public class AppQuickThemeSelectViewComponent : ArchViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass, string iconClass = "flaticon-interface-7 fs-2")
        {
            return Task.FromResult<IViewComponentResult>(View(new QuickThemeSelectionViewModel
            {
                CssClass = cssClass,
                IconClass = iconClass
            }));
        }
    }
}
