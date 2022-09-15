using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Arch
{
    [DependsOn(typeof(ArchXamarinSharedModule))]
    public class ArchXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArchXamarinAndroidModule).GetAssembly());
        }
    }
}