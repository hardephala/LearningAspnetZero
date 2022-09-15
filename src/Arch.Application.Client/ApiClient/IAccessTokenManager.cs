using System;
using System.Threading.Tasks;
using Arch.ApiClient.Models;

namespace Arch.ApiClient
{
    public interface IAccessTokenManager
    {
        string GetAccessToken();

        Task<AbpAuthenticateResultModel> LoginAsync();

        Task<string> RefreshTokenAsync();

        void Logout();

        bool IsUserLoggedIn { get; }

        bool IsRefreshTokenExpired { get; }

        AbpAuthenticateResultModel AuthenticateResult { get; set; }

        DateTime AccessTokenRetrieveTime { get; set; }
    }
}