using Flurl.Http;
using PagSeguro.DotNet.Sdk.Account.Dtos;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Account.Providers
{
    public class AccountProvider : BaseProvider, IAccountProvider
    {
        public AccountProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        public async Task<AccountReadDto> GetAccountByIdAsync(string accountId)
        {
            return await BaseUrl
                .AppendPathSegment(AccountEndpoints.Account)
                .AppendPathSegment(accountId)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientId, Settings.AccessToken)
                .GetJsonAsync<AccountReadDto>();
        }
    }
}
