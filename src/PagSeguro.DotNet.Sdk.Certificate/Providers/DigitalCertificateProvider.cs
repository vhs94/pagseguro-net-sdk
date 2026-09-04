using Flurl.Http;
using PagSeguro.DotNet.Sdk.Certificate.Helpers;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Certificate.Models.Responses;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Certificate.Providers
{
    /// <inheritdoc cref="IDigitalCertificateProvider" />
    public class DigitalCertificateProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IDigitalCertificateProvider
    {
        /// <inheritdoc />
        public async Task<CertificateResponse> CreateAsync()
        {
            EnsureChallenge();

            return await Request()
                .AppendPathSegment(CertificateEndpoints.Certificate)
                .WithOAuthBearerToken(Settings.AccessToken)
                .WithHeader(CertificateHeaders.Challenge, Settings.Challenge)
                .PostAsync()
                .ReceiveJson<CertificateResponse>();
        }
    }
}
