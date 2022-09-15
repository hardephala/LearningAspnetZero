using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Arch.Authorization;

namespace Arch
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(ArchApplicationSharedModule),
        typeof(ArchCoreModule)
        )]
    public class ArchApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArchApplicationModule).GetAssembly());
        }
    }
}