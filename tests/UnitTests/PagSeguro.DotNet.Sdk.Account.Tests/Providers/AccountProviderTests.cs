using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Models.Requests;
using PagSeguro.DotNet.Sdk.Account.Models.Responses;
using PagSeguro.DotNet.Sdk.Account.Providers;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;

namespace PagSeguro.DotNet.Sdk.Account.Tests.Providers
{
    public class AccountProviderTests : BaseProviderTests<AccountProvider>
    {
        private CreatedAccountResponse _createdAccountResponse = null!;
        private AccountResponse _accountResponse = null!;

        protected override AccountProvider CreateProvider()
        {
            return new AccountProvider(Settings, FlurlClientMock);
        }

        protected override void SetupMocks()
        {
            _createdAccountResponse = CreateCreatedAccountResponse();
            _accountResponse = CreateAccountResponse();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account))
                .WithVerb(HttpMethod.Post)
                .RespondWithJson(_createdAccountResponse);
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account, "*"))
                .WithVerb(HttpMethod.Get)
                .RespondWithJson(_accountResponse);
        }

        private CreatedAccountResponse CreateCreatedAccountResponse()
        {
            return Fixture.Create<CreatedAccountResponse>();
        }

        private AccountResponse CreateAccountResponse()
        {
            return Fixture.Create<AccountResponse>();
        }

        [Fact]
        public async Task CreateAsync_AccountIsValid_HttpRequestIsCreated()
        {
            AccountRequest accountRequest = CreateAccountRequest();

            CreatedAccountResponse result = await Provider.CreateAsync(accountRequest);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientId, Settings.ClientId)
                .WithHeader(AccountHeaders.ClientSecret, Settings.ClientSecret)
                .WithRequestJson(accountRequest)
                .WithVerb(HttpMethod.Post)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(
                    _createdAccountResponse,
                    options => options.Excluding(f => f.Person!.BirthDate));
            result.Person!.BirthDate
                .Should()
                .Be(_createdAccountResponse.Person!.BirthDate.Date);

        }

        private AccountRequest CreateAccountRequest()
        {
            return Fixture.Create<AccountRequest>();
        }

        [Fact]
        public async Task CreateAsync_AccessTokenIsEmpty_ClientNotConnectedExceptionIsThrown()
        {
            AccountRequest accountRequest = CreateAccountRequest();
            Settings.AccessToken = null;

            Func<Task> task = async () => await Provider.CreateAsync(accountRequest);

            await task
                .Should()
                .ThrowAsync<ClientNotConnectedException>();
        }

        [Fact]
        public async Task CreateAsync_ClientApplicationIsEmpty_MissingClientApplicationExceptionIsThrown()
        {
            AccountRequest accountRequest = CreateAccountRequest();
            Settings.ClientId = null;
            Settings.ClientSecret = null;

            Func<Task> task = async () => await Provider.CreateAsync(accountRequest);

            await task
                .Should()
                .ThrowAsync<MissingClientApplicationException>();
        }

        [Fact]
        public async Task GetByIdAsync_AccountIdIsValid_HttpRequestIsCreated()
        {
            string accountId = "accountId";

            AccountResponse result = await Provider.GetByIdAsync(accountId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account, accountId))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientToken, Settings.AccessToken)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(
                    _accountResponse,
                    options => options.Excluding(f => f.Person!.BirthDate));
            result.Person!.BirthDate
                .Should()
                .Be(_accountResponse.Person!.BirthDate.Date);
        }

        [Fact]
        public async Task GetByIdAsync_AccessTokenIsEmpty_ClientNotConnectedExceptionIsThrown()
        {
            Settings.AccessToken = null;

            Func<Task> task = async () => await Provider.GetByIdAsync("id");

            await task
                .Should()
                .ThrowAsync<ClientNotConnectedException>();
        }
    }
}
