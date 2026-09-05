using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using System.Text.Json;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class FeeIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CalculateAsync_RequestIsValid_FeesAreReturned()
        {
            FeeResponse feeResponse = CreateFeeResponse();

            FeeResponse result = await Client.ForFee()
                .WithValue(10000)
                .WithMaxInstallments(10)
                .WithMaxInstallmentsNoInterest(4)
                .WithCreditCardBin(552100)
                .CalculateAsync();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(feeResponse);
        }

        private FeeResponse CreateFeeResponse()
            => JsonSerializer.Deserialize<FeeResponse>(File.ReadAllText("Assets/fees.json"))!;
    }
}
