using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.PublicKey.Helpers;
using PagSeguro.DotNet.Sdk.PublicKey.Providers;

namespace PagSeguro.DotNet.Sdk.PublicKey.Tests.Providers
{
    public class PublicKeyProviderTests : BaseProviderTests<PublicKeyProvider>
    {
        protected override PublicKeyProvider CreateProvider()
        {
            return new PublicKeyProvider(Settings);
        }

        [Fact]
        public async Task CreatePublicKeyAsync_RequestIsValid_HttpRequestIsCreated()
        {
            await Provider.CreatePublicKeyAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, PublicKeyEndpoints.PublicKey))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(new
                {
                    type = "card"
                })
                .Times(1);
        }

        [Fact]
        public async Task UpdatePublicKeyAsync_RequestIsValid_HttpRequestIsCreated()
        {
            await Provider.UpdatePublicKeyAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    PublicKeyEndpoints.PublicKey,
                    PublicKeyEndpoints.Card))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Put)
                .Times(1);
        }

        [Fact]
        public async Task GetPublicKeyAsync_RequestIsValid_HttpRequestIsCreated()
        {
            await Provider.GetPublicKeyAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    PublicKeyEndpoints.PublicKey,
                    PublicKeyEndpoints.Card))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }
    }
}
