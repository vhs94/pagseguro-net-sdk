using FluentAssertions;
using PagSeguro.DotNet.Sdk.PublicKey.Models.Responses;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class PublicKeyIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_RequestIsValid_PublicKeyIsCreated()
        {
            PublicKeyResponse result = await Client
                .ForPublicKey()
                .CreateAsync();

            PublicKeyResponse publicKeyResponse = await Client
                .ForPublicKey()
                .GetAsync();
            result
                .Should()
                .NotBeNull();
            result.PublicKey
                .Should()
                .Be(PublicKey);
            result
                .Should()
                .BeEquivalentTo(publicKeyResponse);
        }

        [Fact]
        public async Task UpdateAsync_RequestIsValid_PublicKeyIsUpdated()
        {
            PublicKeyResponse result = await Client
                .ForPublicKey()
                .UpdateAsync();

            result
                .Should()
                .NotBeNull();
            result.PublicKey
                .Should()
                .Be(PublicKey);
        }
    }
}
