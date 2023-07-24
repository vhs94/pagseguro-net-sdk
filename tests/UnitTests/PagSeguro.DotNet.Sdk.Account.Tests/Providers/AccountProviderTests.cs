using AutoFixture;
using Flurl;
using PagSeguro.DotNet.Sdk.Account.Dtos;
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
        public async Task CreateAccountAsync_AccountIdIsValid_HttpRequestIsCreated()
        {
            AccountWriteDto accountWriteDto = CreateAccountWriteDto();

            await Provider.CreateAccountAsync(accountWriteDto);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientId, Settings.ClientId)
                .WithHeader(AccountHeaders.ClientSecret, Settings.ClientSecret)
                .WithRequestJson(accountWriteDto)
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        private AccountWriteDto CreateAccountWriteDto()
        {
            return Fixture.Create<AccountWriteDto>();
        }

        [Fact]
        public async Task GetAccountByIdAsync_AccountIdIsValid_HttpRequestIsCreated()
        {
            string accountId = "accountId";

            await Provider.GetAccountByIdAsync(accountId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account, accountId))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientToken, Settings.AccessToken)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }
    }
}
