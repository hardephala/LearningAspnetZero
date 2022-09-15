using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Arch.Startup
{
    [DependsOn(typeof(ArchCoreModule))]
    public class ArchGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArchGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}