using Flurl.Http;
using PagSeguro.DotNet.Sdk.Account.Dtos;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Attributes;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

[module: RequiredValidation]
namespace PagSeguro.DotNet.Sdk.Account.Providers
{
    public class AccountProvider : BaseProvider, IAccountProvider
    {
        public AccountProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        [AccessTokenRequired]
        [ClientApplicationRequired]
        public async Task<CreatedAccountDto> CreateAsync(AccountWriteDto accountWriteDto)
        {
            return await BaseUrl
                .AppendPathSegment(AccountEndpoints.Account)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientId, Settings.ClientId)
                .WithHeader(AccountHeaders.ClientSecret, Settings.ClientSecret)
                .PostJsonAsync(accountWriteDto)
                .ReceiveJson<CreatedAccountDto>();
        }

        [AccessTokenRequired]
        public async Task<AccountReadDto> GetByIdAsync(string accountId)
        {
            return await BaseUrl
                .AppendPathSegment(AccountEndpoints.Account)
                .AppendPathSegment(accountId)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientToken, Settings.AccessToken)
                .GetJsonAsync<AccountReadDto>();
        }
    }
}
