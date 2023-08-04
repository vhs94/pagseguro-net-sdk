using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.PublicKey.Dtos;
using PagSeguro.DotNet.Sdk.PublicKey.Helpers;
using PagSeguro.DotNet.Sdk.PublicKey.Interfaces;

namespace PagSeguro.DotNet.Sdk.PublicKey.Providers
{
    public class PublicKeyProvider : BaseProvider, IPublicKeyProvider
    {
        public PublicKeyProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        public async Task<PublicKeyReadDto> CreateAsync()
        {
            return await BaseUrl
                .AppendPathSegment(PublicKeyEndpoints.PublicKey)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    type = "card"
                })
                .ReceiveJson<PublicKeyReadDto>();
        }

        public async Task<PublicKeyReadDto> UpdateAsync()
        {
            return await BaseUrl
                .AppendPathSegment(PublicKeyEndpoints.PublicKey)
                .AppendPathSegment(PublicKeyEndpoints.Card)
                .WithOAuthBearerToken(Settings.Token)
                .PutAsync()
                .ReceiveJson<PublicKeyReadDto>();
        }

        public async Task<PublicKeyReadDto> GetAsync()
        {
            return await BaseUrl
                .AppendPathSegment(PublicKeyEndpoints.PublicKey)
                .AppendPathSegment(PublicKeyEndpoints.Card)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<PublicKeyReadDto>();
        }
    }
}
