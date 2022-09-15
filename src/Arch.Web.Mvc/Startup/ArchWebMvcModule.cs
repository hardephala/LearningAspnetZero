using Abp.AspNetZeroCore;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Arch.Auditing;
using Arch.Authorization.Users.Password;
using Arch.Configuration;
using Arch.EntityFrameworkCore;
using Arch.MultiTenancy;
using Arch.Web.Areas.App.Startup;

namespace Arch.Web.Startup
{
    [DependsOn(
        typeof(ArchWebCoreModule)
    )]
    public class ArchWebMvcModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public ArchWebMvcModule(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat = _appConfiguration["App:WebSiteRootAddress"] ?? "https://localhost:44302/";
            Configuration.Modules.AspNetZero().LicenseCode = _appConfiguration["AbpZeroLicenseCode"];
            Configuration.Navigation.Providers.Add<AppNavigationProvider>();
            
            IocManager.Register<DashboardViewConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArchWebMvcModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!IocManager.Resolve<IMultiTenancyConfig>().IsEnabled)
            {
                return;
            }

            using (var scope = IocManager.CreateScope())
            {
                if (!scope.Resolve<DatabaseCheckHelper>().Exist(_appConfiguration["ConnectionStrings:Default"]))
                {
                    return;
                }
            }

            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<SubscriptionExpirationCheckWorker>());
            workManager.Add(IocManager.Resolve<SubscriptionExpireEmailNotifierWorker>());

            if (Configuration.Auditing.IsEnabled && ExpiredAuditLogDeleterWorker.IsEnabled)
            {
                workManager.Add(IocManager.Resolve<ExpiredAuditLogDeleterWorker>());
            }
            
            workManager.Add(IocManager.Resolve<PasswordExpirationBackgroundWorker>());
        }
    }
}