using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.PublicKey.Helpers;
using PagSeguro.DotNet.Sdk.PublicKey.Models.Responses;
using PagSeguro.DotNet.Sdk.PublicKey.Providers;

namespace PagSeguro.DotNet.Sdk.PublicKey.Tests.Providers
{
    public class PublicKeyProviderTests : BaseProviderTests<PublicKeyProvider>
    {
        private PublicKeyResponse _publicKeyResponse = null!;

        protected override PublicKeyProvider CreateProvider()
        {
            return new PublicKeyProvider(Settings, FlurlClientMock);
        }

        protected override void SetupMocks()
        {
            _publicKeyResponse = CreatePublicKeyResponse();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(ProviderBaseUrl, PublicKeyEndpoints.PublicKey),
                    Url.Combine(ProviderBaseUrl, PublicKeyEndpoints.PublicKey, "*"))
                .RespondWithJson(_publicKeyResponse);
        }

        private PublicKeyResponse CreatePublicKeyResponse()
        {
            return Fixture.Create<PublicKeyResponse>();
        }

        [Fact]
        public async Task CreateAsync_RequestIsValid_HttpRequestIsCreated()
        {
            PublicKeyResponse result = await Provider.CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, PublicKeyEndpoints.PublicKey))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(new
                {
                    type = "card"
                })
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_publicKeyResponse);
        }

        [Fact]
        public async Task UpdateAsync_RequestIsValid_HttpRequestIsCreated()
        {
            PublicKeyResponse result = await Provider.UpdateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    ProviderBaseUrl,
                    PublicKeyEndpoints.PublicKey,
                    PublicKeyEndpoints.Card))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Put)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_publicKeyResponse);
        }

        [Fact]
        public async Task GetAsync_RequestIsValid_HttpRequestIsCreated()
        {
            PublicKeyResponse result = await Provider.GetAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    ProviderBaseUrl,
                    PublicKeyEndpoints.PublicKey,
                    PublicKeyEndpoints.Card))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_publicKeyResponse);
        }
    }
}
