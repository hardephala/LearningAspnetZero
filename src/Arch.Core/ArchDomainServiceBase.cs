using Abp.Domain.Services;

namespace Arch
{
    public abstract class ArchDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected ArchDomainServiceBase()
        {
            LocalizationSourceName = ArchConsts.LocalizationSourceName;
        }
    }
}
