using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Providers.Cards;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Cards
{
    public class AuthenticationSessionProviderTests : BaseProviderTests<AuthenticationSessionProvider>
    {
        protected override AuthenticationSessionProvider CreateProvider()
        {
            return new AuthenticationSessionProvider(Settings, FlurlClientMock);
        }

        protected override void CreateMocks()
        {
        }

        // A sessao 3DS nao fica na API principal e sim no host do SDK de
        // front-end, entao os testes de URL base herdados sao sobrescritos.
        [Fact]
        public override void BaseUrl_EnvironmentIsSandbox_SandboxUrlIsAssigned()
        {
            ProviderBaseUrl.ToString().Should().Be(OrderEndpoint.SandboxSdkBaseUrl);
        }

        [Fact]
        public override void BaseUrl_EnvironmentIsProduction_ProductionUrlIsAssigned()
        {
            Settings.Environment = PagSeguroEnvironment.Production;

            ProviderBaseUrl.ToString().Should().Be(OrderEndpoint.ProductionSdkBaseUrl);
        }

        [Fact]
        public async Task CreateAsync_Always_HttpRequestIsCreated()
        {
            AuthenticationSessionResponse sessionResponse =
                Fixture.Create<AuthenticationSessionResponse>();
            string url = Url.Combine(ProviderBaseUrl, OrderEndpoint.AuthenticationSessions);
            HttpTestMock.ForCallsTo(url).RespondWithJson(sessionResponse);

            AuthenticationSessionResponse result = await Provider.CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(url)
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Post)
                .Times(1);
            result.Should().BeEquivalentTo(sessionResponse);
        }

        [Fact]
        public async Task CreateAsync_ResponseIsReturned_ExpiresAtIsDeserialized()
        {
            string url = Url.Combine(ProviderBaseUrl, OrderEndpoint.AuthenticationSessions);
            HttpTestMock.ForCallsTo(url).RespondWith(
                """
                { "session": "token-3ds", "expires_at": 1788648039000 }
                """);

            AuthenticationSessionResponse result = await Provider.CreateAsync();

            result.Session.Should().Be("token-3ds");
            result.ExpiresAt.Should().Be(1788648039000);
        }
    }
}
