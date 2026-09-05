using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    /// <summary>
    /// Cobertura viva de POST /tokens/cards.
    /// </summary>
    public class CardTokenIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_CardIsValid_CardIsStored()
        {
            CardTokenRequest cardTokenRequest = new()
            {
                Number = "4111111111111111",
                ExpMonth = "12",
                ExpYear = "2030",
                SecurityCode = "123",
                Holder = new CardTokenHolder { Name = "Jose da Silva", TaxId = "12345678909" }
            };

            CardTokenResponse result = await Client.ForCardToken().CreateAsync(cardTokenRequest);

            result.Should().NotBeNull();
            result.Id.Should().StartWith("CARD_");
            result.Brand.Should().Be("visa");
            result.FirstDigits.Should().Be("411111");
            result.LastDigits.Should().Be("1111");
            result.ExpMonth.Should().Be("12");
            result.ExpYear.Should().Be("2030");
            result.Holder!.Name.Should().Be("Jose da Silva");
        }

        [Fact]
        public async Task CreateAsync_CardNumberIsInvalid_ApiRejectsTheCard()
        {
            CardTokenRequest cardTokenRequest = new()
            {
                Number = "1111111111111111",
                ExpMonth = "12",
                ExpYear = "2030",
                SecurityCode = "123",
                Holder = new CardTokenHolder { Name = "Jose da Silva" }
            };

            Func<Task> task = async () => await Client.ForCardToken().CreateAsync(cardTokenRequest);

            await task.Should().ThrowAsync<BadRequestException>();
        }
    }
}
