using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Connect.Providers
{
    /// <inheritdoc cref="IApplicationProvider" />
    public class ApplicationProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IApplicationProvider
    {
        /// <inheritdoc />
        public async Task<ApplicationResponse> CreateAsync(ApplicationRequest applicationRequest)
        {
            return await Request()
                .AppendPathSegment(ConnectEndpoints.Application)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(applicationRequest)
                .ReceiveJson<ApplicationResponse>();
        }

        /// <inheritdoc />
        public async Task<ApplicationResponse> GetByClientIdAsync(string clientId)
        {
            return await Request()
                .AppendPathSegment($"{ConnectEndpoints.Application}/{clientId}")
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ApplicationResponse>();
        }
    }
}
