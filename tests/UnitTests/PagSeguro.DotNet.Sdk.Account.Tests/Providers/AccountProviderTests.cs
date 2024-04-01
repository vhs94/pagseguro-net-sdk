using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Account.Dtos;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Providers;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;

namespace PagSeguro.DotNet.Sdk.Account.Tests.Providers
{
    public class AccountProviderTests : BaseProviderTests<AccountProvider>
    {
        private CreatedAccountDto _createdAccountDto;
        private AccountReadDto _accountReadDto;

        protected override AccountProvider CreateProvider()
        {
            return new AccountProvider(Settings);
        }

        protected override void SetupMocks()
        {
            _createdAccountDto = CreateCreatedAccountDto();
            _accountReadDto = CreateAccountReadDto();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account))
                .WithVerb(HttpMethod.Post)
                .RespondWithJson(_createdAccountDto);
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account, "*"))
                .WithVerb(HttpMethod.Get)
                .RespondWithJson(_accountReadDto);
        }

        private CreatedAccountDto CreateCreatedAccountDto()
        {
            return Fixture.Create<CreatedAccountDto>();
        }

        private AccountReadDto CreateAccountReadDto()
        {
            return Fixture.Create<AccountReadDto>();
        }

        [Fact]
        public async Task CreateAsync_AccountIsValid_HttpRequestIsCreated()
        {
            AccountWriteDto accountWriteDto = CreateAccountWriteDto();

            CreatedAccountDto result = await Provider.CreateAsync(accountWriteDto);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientId, Settings.ClientId)
                .WithHeader(AccountHeaders.ClientSecret, Settings.ClientSecret)
                .WithRequestJson(accountWriteDto)
                .WithVerb(HttpMethod.Post)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(
                    _createdAccountDto,
                    options => options.Excluding(f => f.Person.BirthDate));
            result.Person.BirthDate
                .Should()
                .Be(_createdAccountDto.Person.BirthDate.Date);

        }

        private AccountWriteDto CreateAccountWriteDto()
        {
            return Fixture.Create<AccountWriteDto>();
        }

        [Fact]
        public async Task CreateAsync_AccessTokenIsEmpty_ClientNotConnectedExceptionIsThrown()
        {
            AccountWriteDto accountWriteDto = CreateAccountWriteDto();
            Settings.AccessToken = null;

            Func<Task> task = async () => await Provider.CreateAsync(accountWriteDto);

            await task
                .Should()
                .ThrowAsync<ClientNotConnectedException>();
        }

        [Fact]
        public async Task CreateAsync_ClientApplicationIsEmpty_MissingClientApplicationExceptionIsThrown()
        {
            AccountWriteDto accountWriteDto = CreateAccountWriteDto();
            Settings.ClientId = null;
            Settings.ClientSecret = null;

            Func<Task> task = async () => await Provider.CreateAsync(accountWriteDto);

            await task
                .Should()
                .ThrowAsync<MissingClientApplicationException>();
        }

        [Fact]
        public async Task GetByIdAsync_AccountIdIsValid_HttpRequestIsCreated()
        {
            string accountId = "accountId";

            AccountReadDto result = await Provider.GetByIdAsync(accountId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, AccountEndpoints.Account, accountId))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(AccountHeaders.ClientToken, Settings.AccessToken)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(
                    _accountReadDto,
                    options => options.Excluding(f => f.Person.BirthDate));
            result.Person.BirthDate
                .Should()
                .Be(_accountReadDto.Person.BirthDate.Date);
        }

        [Fact]
        public async Task GetByIdAsync_AccessTokenIsEmpty_ClientNotConnectedExceptionIsThrown()
        {
            Settings.AccessToken = null;

            Func<Task> task = async () => await Provider.GetByIdAsync(null);

            await task
                .Should()
                .ThrowAsync<ClientNotConnectedException>();
        }
    }
}
