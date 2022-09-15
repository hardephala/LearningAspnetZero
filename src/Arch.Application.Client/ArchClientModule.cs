using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Arch
{
    public class ArchClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArchClientModule).GetAssembly());
        }
    }
}
