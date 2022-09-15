using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Arch.Web.Public.Views
{
    public abstract class ArchRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected ArchRazorPage()
        {
            LocalizationSourceName = ArchConsts.LocalizationSourceName;
        }
    }
}
