using Abp;

namespace Arch
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// It's suitable for non domain nor application service classes.
    /// For domain services inherit <see cref="ArchDomainServiceBase"/>.
    /// For application services inherit ArchAppServiceBase.
    /// </summary>
    public abstract class ArchServiceBase : AbpServiceBase
    {
        protected ArchServiceBase()
        {
            LocalizationSourceName = ArchConsts.LocalizationSourceName;
        }
    }
}