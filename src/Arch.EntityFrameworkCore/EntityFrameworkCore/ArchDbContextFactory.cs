using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Arch.Configuration;
using Arch.Web;

namespace Arch.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ArchDbContextFactory : IDesignTimeDbContextFactory<ArchDbContext>
    {
        public ArchDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ArchDbContext>();

            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder(),
                addUserSecrets: true
            );

            ArchDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ArchConsts.ConnectionStringName));

            return new ArchDbContext(builder.Options);
        }
    }
}
