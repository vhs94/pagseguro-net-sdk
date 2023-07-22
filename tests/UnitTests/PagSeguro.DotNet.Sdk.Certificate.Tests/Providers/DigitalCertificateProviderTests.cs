using Flurl;
using PagSeguro.DotNet.Sdk.Certificate.Helpers;
using PagSeguro.DotNet.Sdk.Certificate.Providers;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;

namespace PagSeguro.DotNet.Sdk.Certificate.Tests.Providers
{
    public class DigitalCertificateProviderTests : BaseProviderTests<DigitalCertificateProvider>
    {
        protected override DigitalCertificateProvider CreateProvider()
        {
            return new DigitalCertificateProvider(Settings);
        }

        [Fact]
        public async Task CreateCertificateAsync_CertificateIsValid_HttpRequestIsCreated()
        {
            await Provider.CreateCertificateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, CertificateEndpoints.Certificate))
                .WithOAuthBearerToken(Settings.AccessToken)
                .WithHeader(CertificateHeaders.Challenge, Settings.Challenge)
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }
    }
}
