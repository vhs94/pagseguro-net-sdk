using Flurl;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Providers;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;

namespace PagSeguro.DotNet.Sdk.Account.Tests.Providers
{
    public class AccountProviderTests : BaseProviderTests<AccountProvider>
    {
        protected override AccountProvider CreateProvider()
        {
            return new AccountProvider(Settings);
        }

        [Fact]
        public async Task GetAccountByIdAsync_AccountIdIsValidIsValid_HttpRequestIsCreated()
        {
            string accountId = "accountId";

            await Provider.GetAccountByIdAsync(accountId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account, accountId))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientId, Settings.AccessToken)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }
    }
}
