using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Account.Dtos;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode;

namespace PagSeguro.DotNet.Sdk.IntegrationTests
{
    public class AccountTests : BaseIntegrationTests
    {

        [Fact]
        public async Task CreateAsync_AccountIsValid_AccountIsCreated()
        {
            await ConnectAsync();
            AccountWriteDto accountWriteDto = CreateAccountWriteDto();

            CreatedAccountDto result = await Client
                .ForAccount()
                .CreateAsync(accountWriteDto);

            result
                .CreateDate.Date
                .Should()
                .Be(DateTime.UtcNow.Date);

        }

        private AccountWriteDto CreateAccountWriteDto()
        {
            return Fixture.Create<AccountWriteDto>();
        }
    }
}
