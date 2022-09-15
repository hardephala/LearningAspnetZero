﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp;
using Abp.UI;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using Abp.Timing;
using Microsoft.IdentityModel.Tokens;
using Arch.Authorization.Users;
using Arch.Authorization.Delegation;
using Arch.Authorization;

namespace Arch.Web.Authentication.JwtBearer
{
    public class ArchAsyncJwtSecurityTokenHandler : IAsyncSecurityTokenValidator
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public ArchAsyncJwtSecurityTokenHandler()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

        public bool CanReadToken(string securityToken)
        {
            return _tokenHandler.CanReadToken(securityToken);
        }

        public async Task<(ClaimsPrincipal, SecurityToken)> ValidateToken(string securityToken,
            TokenValidationParameters validationParameters)
        {
            var cacheManager = IocManager.Instance.Resolve<ICacheManager>();
            var principal = _tokenHandler.ValidateToken(securityToken, validationParameters, out var validatedToken);

            if (!HasAccessTokenType(principal))
            {
                throw new SecurityTokenException("invalid token type");
            }

            await ValidateSecurityStampAsync(principal);

            var tokenValidityKeyClaim = principal.Claims.First(c => c.Type == AppConsts.TokenValidityKey);
            if (TokenValidityKeyExistsInCache(tokenValidityKeyClaim, cacheManager))
            {
                return (principal, validatedToken);
            }

            var userIdentifierString = principal.Claims.First(c => c.Type == AppConsts.UserIdentifier);
            var userIdentifier = UserIdentifier.Parse(userIdentifierString.Value);

            if (!await ValidateTokenValidityKey(tokenValidityKeyClaim, userIdentifier))
            {
                throw new SecurityTokenException("invalid");
            }

            var tokenAuthConfiguration = IocManager.Instance.Resolve<TokenAuthConfiguration>();

            await cacheManager.GetCache(AppConsts.TokenValidityKey).SetAsync(
                tokenValidityKeyClaim.Value, "",
                absoluteExpireTime: new DateTimeOffset(
                    Clock.Now.AddMinutes(tokenAuthConfiguration.AccessTokenExpiration.TotalMinutes)
                )
            );

            return (principal, validatedToken);
        }

        private async Task<bool> ValidateTokenValidityKey(Claim tokenValidityKeyClaim, UserIdentifier userIdentifier)
        {
            bool isValid;

            using (var unitOfWorkManager = IocManager.Instance.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = unitOfWorkManager.Object.Begin())
                {
                    using (unitOfWorkManager.Object.Current.SetTenantId(userIdentifier.TenantId))
                    {
                        using (var userManager = IocManager.Instance.ResolveAsDisposable<UserManager>())
                        {
                            var userManagerObject = userManager.Object;
                            var user = await userManagerObject.GetUserAsync(userIdentifier);
                            isValid = await userManagerObject.IsTokenValidityKeyValidAsync(
                                user,
                                tokenValidityKeyClaim.Value
                            );

                            await uow.CompleteAsync();
                        }
                    }
                }
            }

            return isValid;
        }

        private static bool TokenValidityKeyExistsInCache(Claim tokenValidityKeyClaim, ICacheManager cacheManager)
        {
            var tokenValidityKeyInCache = cacheManager
                .GetCache(AppConsts.TokenValidityKey)
                .GetOrDefault(tokenValidityKeyClaim.Value);

            return tokenValidityKeyInCache != null;
        }

        private static async Task ValidateSecurityStampAsync(ClaimsPrincipal principal)
        {
            ValidateUserDelegation(principal);

            using (var securityStampHandler = IocManager.Instance.ResolveAsDisposable<IJwtSecurityStampHandler>())
            {
                if (!await securityStampHandler.Object.Validate(principal))
                {
                    throw new SecurityTokenException("invalid");
                }
            }
        }

        private bool HasAccessTokenType(ClaimsPrincipal principal)
        {
            return principal.Claims.FirstOrDefault(x => x.Type == AppConsts.TokenType)?.Value ==
                   TokenType.AccessToken.To<int>().ToString();
        }

        private static void ValidateUserDelegation(ClaimsPrincipal principal)
        {
            var userDelegationConfiguration = IocManager.Instance.Resolve<IUserDelegationConfiguration>();
            if (!userDelegationConfiguration.IsEnabled)
            {
                return;
            }

            var impersonatorTenant = principal.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorTenantId);
            var user = principal.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.UserId);
            var impersonatorUser = principal.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorUserId);

            if (impersonatorUser == null || user == null)
            {
                return;
            }

            var impersonatorTenantId = impersonatorTenant == null ? null :
                impersonatorTenant.Value.IsNullOrEmpty() ? (int?)null : Convert.ToInt32(impersonatorTenant.Value);
            var sourceUserId = Convert.ToInt64(user.Value);
            var impersonatorUserId = Convert.ToInt64(impersonatorUser.Value);

            using (var permissionChecker = IocManager.Instance.ResolveAsDisposable<PermissionChecker>())
            {
                if (permissionChecker.Object.IsGranted(
                        new UserIdentifier(impersonatorTenantId, impersonatorUserId),
                        AppPermissions.Pages_Administration_Users_Impersonation)
                   )
                {
                    return;
                }
            }

            using (var userDelegationManager = IocManager.Instance.ResolveAsDisposable<IUserDelegationManager>())
            {
                var hasActiveDelegation = userDelegationManager.Object.HasActiveDelegation(
                    sourceUserId,
                    impersonatorUserId
                );

                if (!hasActiveDelegation)
                {
                    throw new UserFriendlyException("ThereIsNoActiveUserDelegationBetweenYourUserAndCurrentUser");
                }
            }
        }
    }
}
