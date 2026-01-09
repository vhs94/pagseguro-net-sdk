using Flurl.Http;
using PagSeguro.DotNet.Sdk.Certificate.Dtos;
using PagSeguro.DotNet.Sdk.Certificate.Helpers;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Certificate.Providers
{
    public class DigitalCertificateProvider(PagSeguroSettings settings)
        : BaseProvider(settings),
        IDigitalCertificateProvider
    {
        public async Task<CertificateReadDto> CreateAsync()
        {
            EnsureChallenge();

            return await BaseUrl
                .AppendPathSegment(CertificateEndpoints.Certificate)
                .WithOAuthBearerToken(Settings.AccessToken)
                .WithHeader(CertificateHeaders.Challenge, Settings.Challenge)
                .PostAsync()
                .ReceiveJson<CertificateReadDto>();
        }
    }
}
