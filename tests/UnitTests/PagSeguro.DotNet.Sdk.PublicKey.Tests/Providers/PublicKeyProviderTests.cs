using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.PublicKey.Dtos;
using PagSeguro.DotNet.Sdk.PublicKey.Helpers;
using PagSeguro.DotNet.Sdk.PublicKey.Providers;

namespace PagSeguro.DotNet.Sdk.PublicKey.Tests.Providers
{
    public class PublicKeyProviderTests : BaseProviderTests<PublicKeyProvider>
    {
        private PublicKeyReadDto _publicKeyReadDto = null!;

        protected override PublicKeyProvider CreateProvider()
        {
            return new PublicKeyProvider(Settings);
        }

        protected override void SetupMocks()
        {
            _publicKeyReadDto = CreatePublicKeyReadDto();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, PublicKeyEndpoints.PublicKey),
                    Url.Combine(Provider.BaseUrl, PublicKeyEndpoints.PublicKey, "*"))
                .RespondWithJson(_publicKeyReadDto);
        }

        private PublicKeyReadDto CreatePublicKeyReadDto()
        {
            return Fixture.Create<PublicKeyReadDto>();
        }

        [Fact]
        public async Task CreateAsync_RequestIsValid_HttpRequestIsCreated()
        {
            PublicKeyReadDto result = await Provider.CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, PublicKeyEndpoints.PublicKey))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(new
                {
                    type = "card"
                })
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_publicKeyReadDto);
        }

        [Fact]
        public async Task UpdateAsync_RequestIsValid_HttpRequestIsCreated()
        {
            PublicKeyReadDto result = await Provider.UpdateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    PublicKeyEndpoints.PublicKey,
                    PublicKeyEndpoints.Card))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Put)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_publicKeyReadDto);
        }

        [Fact]
        public async Task GetAsync_RequestIsValid_HttpRequestIsCreated()
        {
            PublicKeyReadDto result = await Provider.GetAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    PublicKeyEndpoints.PublicKey,
                    PublicKeyEndpoints.Card))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_publicKeyReadDto);
        }
    }
}
