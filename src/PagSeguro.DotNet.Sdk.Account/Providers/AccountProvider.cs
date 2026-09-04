using Flurl.Http;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Account.Models.Requests;
using PagSeguro.DotNet.Sdk.Account.Models.Responses;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Account.Providers
{
    /// <inheritdoc cref="IAccountProvider" />
    public class AccountProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IAccountProvider
    {
        /// <inheritdoc />
        public async Task<CreatedAccountResponse> CreateAsync(AccountRequest accountRequest)
        {
            EnsureAccessToken();
            EnsureClientApplication();

            return await Request()
                .AppendPathSegment(AccountEndpoints.Account)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientId, Settings.ClientId)
                .WithHeader(AccountHeaders.ClientSecret, Settings.ClientSecret)
                .PostJsonAsync(accountRequest)
                .ReceiveJson<CreatedAccountResponse>();
        }

        /// <inheritdoc />
        public async Task<AccountResponse> GetByIdAsync(string accountId)
        {
            EnsureAccessToken();

            return await Request()
                .AppendPathSegment(AccountEndpoints.Account)
                .AppendPathSegment(accountId)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientToken, Settings.AccessToken)
                .GetJsonAsync<AccountResponse>();
        }
    }
}
