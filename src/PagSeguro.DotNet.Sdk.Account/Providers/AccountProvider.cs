using Flurl.Http;
using PagSeguro.DotNet.Sdk.Account.Dtos;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Account.Providers
{
    public class AccountProvider(PagSeguroSettings settings)
        : BaseProvider(settings),
        IAccountProvider
    {
        public async Task<CreatedAccountDto> CreateAsync(AccountWriteDto accountWriteDto)
        {
            EnsureAccessToken();
            EnsureClientApplication();

            return await BaseUrl
                .AppendPathSegment(AccountEndpoints.Account)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientId, Settings.ClientId)
                .WithHeader(AccountHeaders.ClientSecret, Settings.ClientSecret)
                .PostJsonAsync(accountWriteDto)
                .ReceiveJson<CreatedAccountDto>();
        }

        public async Task<AccountReadDto> GetByIdAsync(string accountId)
        {
            EnsureAccessToken();

            return await BaseUrl
                .AppendPathSegment(AccountEndpoints.Account)
                .AppendPathSegment(accountId)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientToken, Settings.AccessToken)
                .GetJsonAsync<AccountReadDto>();
        }
    }
}
