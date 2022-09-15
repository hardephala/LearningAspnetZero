using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Arch
{
    [DependsOn(typeof(ArchClientModule), typeof(AbpAutoMapperModule))]
    public class ArchXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArchXamarinSharedModule).GetAssembly());
        }
    }
}