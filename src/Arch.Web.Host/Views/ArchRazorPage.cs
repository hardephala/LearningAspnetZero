using Abp.AspNetCore.Mvc.Views;

namespace Arch.Web.Views
{
    public abstract class ArchRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected ArchRazorPage()
        {
            LocalizationSourceName = ArchConsts.LocalizationSourceName;
        }
    }
}
