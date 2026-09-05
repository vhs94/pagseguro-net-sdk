using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using PagSeguro.DotNet.Sdk.Orders.Providers.Cards;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Cards
{
    public class CardTokenProviderTests : BaseProviderTests<CardTokenProvider>
    {
        protected override CardTokenProvider CreateProvider()
        {
            return new CardTokenProvider(Settings, FlurlClientMock);
        }

        protected override void CreateMocks()
        {
        }

        [Fact]
        public async Task CreateAsync_CardIsValid_HttpRequestIsCreated()
        {
            CardTokenResponse cardTokenResponse = Fixture.Create<CardTokenResponse>();
            string url = Url.Combine(ProviderBaseUrl, OrderEndpoint.CardTokens);
            HttpTestMock.ForCallsTo(url).RespondWithJson(cardTokenResponse);
            CardTokenRequest cardTokenRequest = CreateCardTokenRequest();

            CardTokenResponse result = await Provider.CreateAsync(cardTokenRequest);

            HttpTestMock
                .ShouldHaveCalled(url)
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(cardTokenRequest)
                .Times(1);
            result.Should().BeEquivalentTo(cardTokenResponse);
        }

        [Fact]
        public async Task CreateAsync_EncryptedCardIsUsed_OpenCardFieldsAreOmitted()
        {
            // No caminho criptografado a API recusa a presenca dos campos abertos.
            string url = Url.Combine(ProviderBaseUrl, OrderEndpoint.CardTokens);
            HttpTestMock.ForCallsTo(url).RespondWithJson(Fixture.Create<CardTokenResponse>());

            await Provider.CreateAsync(new CardTokenRequest { Encrypted = "criptografado" });

            HttpTestMock
                .ShouldHaveCalled(url)
                .With(call => !call.RequestBody.Contains("number")
                    && !call.RequestBody.Contains("exp_month")
                    && !call.RequestBody.Contains("security_code"))
                .Times(1);
        }

        [Fact]
        public async Task CreateAsync_HolderIsInformed_TaxIdUsesTheApiFieldName()
        {
            string url = Url.Combine(ProviderBaseUrl, OrderEndpoint.CardTokens);
            HttpTestMock.ForCallsTo(url).RespondWithJson(Fixture.Create<CardTokenResponse>());

            await Provider.CreateAsync(CreateCardTokenRequest());

            HttpTestMock
                .ShouldHaveCalled(url)
                .With(call => call.RequestBody.Contains("tax_id")
                    && call.RequestBody.Contains("exp_month")
                    && call.RequestBody.Contains("exp_year"))
                .Times(1);
        }

        private static CardTokenRequest CreateCardTokenRequest()
        {
            return new CardTokenRequest
            {
                Number = "4111111111111111",
                ExpMonth = "12",
                ExpYear = "2030",
                SecurityCode = "123",
                Holder = new CardTokenHolder { Name = "Jose da Silva", TaxId = "12345678909" }
            };
        }
    }
}
