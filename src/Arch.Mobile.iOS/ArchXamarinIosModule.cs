using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Arch
{
    [DependsOn(typeof(ArchXamarinSharedModule))]
    public class ArchXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArchXamarinIosModule).GetAssembly());
        }
    }
}