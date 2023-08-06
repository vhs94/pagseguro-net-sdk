using FluentAssertions;
using PagSeguro.DotNet.Sdk.PublicKey.Dtos;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class PublicKeyIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_RequestIsValid_PublicKeyIsCreated()
        {
            PublicKeyReadDto result = await Client
                .ForPublicKey()
                .CreateAsync();

            PublicKeyReadDto publicKeyReadDto = await Client
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
                .BeEquivalentTo(publicKeyReadDto);
        }

        [Fact]
        public async Task UpdateAsync_RequestIsValid_PublicKeyIsUpdated()
        {
            PublicKeyReadDto result = await Client
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
