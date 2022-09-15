using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Arch.Configure;
using Arch.Startup;
using Arch.Test.Base;

namespace Arch.GraphQL.Tests
{
    [DependsOn(
        typeof(ArchGraphQLModule),
        typeof(ArchTestBaseModule))]
    public class ArchGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArchGraphQLTestModule).GetAssembly());
        }
    }
}