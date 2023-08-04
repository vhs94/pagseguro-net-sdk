using Flurl.Http;
using PagSeguro.DotNet.Sdk.Certificate.Dtos;
using PagSeguro.DotNet.Sdk.Certificate.Helpers;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Attributes;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

[module: RequiredValidation]
namespace PagSeguro.DotNet.Sdk.Certificate.Providers
{
    public class DigitalCertificateProvider : BaseProvider, IDigitalCertificateProvider
    {
        public DigitalCertificateProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        [ChallengeRequired]
        public async Task<CertificateReadDto> CreateAsync()
        {
            return await BaseUrl
                .AppendPathSegment(CertificateEndpoints.Certificate)
                .WithOAuthBearerToken(Settings.AccessToken)
                .WithHeader(CertificateHeaders.Challenge, Settings.Challenge)
                .PostAsync()
                .ReceiveJson<CertificateReadDto>();
        }
    }
}
