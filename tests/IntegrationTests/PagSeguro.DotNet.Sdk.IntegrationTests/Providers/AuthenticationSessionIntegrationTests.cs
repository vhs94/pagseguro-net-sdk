using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    /// <summary>
    /// Cobertura viva de POST /checkout-sdk/sessions. Este endpoint fica em um
    /// host proprio (sdk.pagseguro.com), e nao na API principal.
    /// </summary>
    public class AuthenticationSessionIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_Always_SessionIsCreated()
        {
            AuthenticationSessionResponse result = await Client
                .ForAuthenticationSession()
                .CreateAsync();

            result.Should().NotBeNull();
            result.Session.Should().NotBeNullOrWhiteSpace();

            // A sessao vale por 30 minutos, entao a expiracao tem que estar no futuro.
            DateTimeOffset expiresAt = DateTimeOffset.FromUnixTimeMilliseconds(result.ExpiresAt);
            expiresAt.Should().BeAfter(DateTimeOffset.UtcNow);
            expiresAt.Should().BeBefore(DateTimeOffset.UtcNow.AddHours(2));
        }

        [Fact]
        public async Task CreateAsync_CalledTwice_SessionsAreDifferent()
        {
            AuthenticationSessionResponse first = await Client
                .ForAuthenticationSession()
                .CreateAsync();
            AuthenticationSessionResponse second = await Client
                .ForAuthenticationSession()
                .CreateAsync();

            second.Session.Should().NotBe(first.Session);
        }
    }
}
