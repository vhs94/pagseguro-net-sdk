using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Connect.Providers
{
    public class ApplicationProvider(PagSeguroSettings settings)
        : BaseProvider(settings),
        IApplicationProvider
    {
        public async Task<ApplicationResponse> CreateAsync(ApplicationRequest applicationRequest)
        {
            return await BaseUrl
                .AppendPathSegment(ConnectEndpoints.Application)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(applicationRequest)
                .ReceiveJson<ApplicationResponse>();
        }

        public async Task<ApplicationResponse> GetByClientIdAsync(string clientId)
        {
            return await BaseUrl
                .AppendPathSegment($"{ConnectEndpoints.Application}/{clientId}")
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ApplicationResponse>();
        }
    }
}
