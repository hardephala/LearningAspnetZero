using System.Globalization;
using Abp.Dependency;

namespace Arch.Localization
{
    public class ApplicationCulturesProvider : IApplicationCulturesProvider, ITransientDependency
    {
        public CultureInfo[] GetAllCultures()
        {
            return CultureInfo.GetCultures(CultureTypes.AllCultures);
        }
    }
}
