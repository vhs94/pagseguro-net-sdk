using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Fees;
using System.Text.Json;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class FeeIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CalculateAsync_RequestIsValid_FeesAreReturned()
        {
            FeeReadDto feeReadDto = CreateFeeReadDto();

            FeeReadDto result = await Client.ForFee()
                .WithValue(10000)
                .WithMaxInstallments(10)
                .WithMaxInstallmentsNoInterest(4)
                .WithCreditCardBin(552100)
                .CalculateAsync();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(feeReadDto);
        }

        private FeeReadDto CreateFeeReadDto()
            => JsonSerializer.Deserialize<FeeReadDto>(File.ReadAllText("Assets/fees.json"))!;
    }
}
