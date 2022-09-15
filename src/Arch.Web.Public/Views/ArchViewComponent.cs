using Abp.AspNetCore.Mvc.ViewComponents;

namespace Arch.Web.Public.Views
{
    public abstract class ArchViewComponent : AbpViewComponent
    {
        protected ArchViewComponent()
        {
            LocalizationSourceName = ArchConsts.LocalizationSourceName;
        }
    }
}