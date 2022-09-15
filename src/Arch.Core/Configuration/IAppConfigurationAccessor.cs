using Microsoft.Extensions.Configuration;

namespace Arch.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
