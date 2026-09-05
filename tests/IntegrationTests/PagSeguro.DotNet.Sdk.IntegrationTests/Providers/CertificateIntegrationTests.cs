using FluentAssertions;
using PagSeguro.DotNet.Sdk.Certificate.Models.Responses;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class CertificateIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_RequestIsValid_CertificateIsCreated()
        {
            await Client.ConnectChallengeAsync();

            CertificateResponse result = await Client
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
