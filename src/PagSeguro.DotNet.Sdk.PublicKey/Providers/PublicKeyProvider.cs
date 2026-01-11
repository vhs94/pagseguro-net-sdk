using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.PublicKey.Helpers;
using PagSeguro.DotNet.Sdk.PublicKey.Interfaces;
using PagSeguro.DotNet.Sdk.PublicKey.Models.Response;

namespace PagSeguro.DotNet.Sdk.PublicKey.Providers
{
    public class PublicKeyProvider(PagSeguroSettings settings)
        : BaseProvider(settings),
        IPublicKeyProvider
    {
        public async Task<PublicKeyResponse> CreateAsync()
        {
            return await BaseUrl
                .AppendPathSegment(PublicKeyEndpoints.PublicKey)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    type = "card"
                })
                .ReceiveJson<PublicKeyResponse>();
        }

        public async Task<PublicKeyResponse> UpdateAsync()
        {
            return await BaseUrl
                .AppendPathSegment(PublicKeyEndpoints.PublicKey)
                .AppendPathSegment(PublicKeyEndpoints.Card)
                .WithOAuthBearerToken(Settings.Token)
                .PutAsync()
                .ReceiveJson<PublicKeyResponse>();
        }

        public async Task<PublicKeyResponse> GetAsync()
        {
            return await BaseUrl
                .AppendPathSegment(PublicKeyEndpoints.PublicKey)
                .AppendPathSegment(PublicKeyEndpoints.Card)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<PublicKeyResponse>();
        }
    }
}
