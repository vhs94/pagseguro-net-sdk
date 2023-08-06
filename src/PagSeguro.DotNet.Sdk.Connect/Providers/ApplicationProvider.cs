using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Application;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;

namespace PagSeguro.DotNet.Sdk.Connect.Providers
{
    public class ApplicationProvider : BaseProvider, IApplicationProvider
    {
        public ApplicationProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        public async Task<ApplicationReadDto> CreateAsync(ApplicationWriteDto applicationWriteDto)
        {
            return await BaseUrl
                .AppendPathSegment(ConnectEndpoints.Application)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(applicationWriteDto)
                .ReceiveJson<ApplicationReadDto>();
        }

        public async Task<ApplicationReadDto> GetByClientIdAsync(string clientId)
        {
            return await BaseUrl
                .AppendPathSegment($"{ConnectEndpoints.Application}/{clientId}")
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ApplicationReadDto>();
        }
    }
}
