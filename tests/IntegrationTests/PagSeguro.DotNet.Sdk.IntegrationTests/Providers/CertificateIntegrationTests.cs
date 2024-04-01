using FluentAssertions;
using PagSeguro.DotNet.Sdk.Certificate.Dtos;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class CertificateIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_RequestIsValid_CertificateIsCreated()
        {
            await Client.ConnectChallengeAsync();

            CertificateReadDto result = await Client
                .ForCertificate()
                .CreateAsync();

            result
                .Should()
                .NotBeNull();
            result.Id
                .Should()
                .StartWith("CERT:");
        }
    }
}
