using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Arch
{
    [DependsOn(typeof(ArchCoreSharedModule))]
    public class ArchApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArchApplicationSharedModule).GetAssembly());
        }
    }
}