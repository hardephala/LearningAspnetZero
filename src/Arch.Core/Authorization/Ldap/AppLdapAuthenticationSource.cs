using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Arch.Authorization.Users;
using Arch.MultiTenancy;

namespace Arch.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}