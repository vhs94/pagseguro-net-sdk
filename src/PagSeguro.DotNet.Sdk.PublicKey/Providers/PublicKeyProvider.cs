using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.PublicKey.Helpers;
using PagSeguro.DotNet.Sdk.PublicKey.Interfaces;
using PagSeguro.DotNet.Sdk.PublicKey.Models.Responses;

namespace PagSeguro.DotNet.Sdk.PublicKey.Providers
{
    /// <inheritdoc cref="IPublicKeyProvider" />
    public class PublicKeyProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IPublicKeyProvider
    {
        /// <inheritdoc />
        public async Task<PublicKeyResponse> CreateAsync()
        {
            return await Request()
                .AppendPathSegment(PublicKeyEndpoints.PublicKey)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    type = "card"
                })
                .ReceiveJson<PublicKeyResponse>();
        }

        /// <inheritdoc />
        public async Task<PublicKeyResponse> UpdateAsync()
        {
            return await Request()
                .AppendPathSegment(PublicKeyEndpoints.PublicKey)
                .AppendPathSegment(PublicKeyEndpoints.Card)
                .WithOAuthBearerToken(Settings.Token)
                .PutAsync()
                .ReceiveJson<PublicKeyResponse>();
        }

        /// <inheritdoc />
        public async Task<PublicKeyResponse> GetAsync()
        {
            return await Request()
                .AppendPathSegment(PublicKeyEndpoints.PublicKey)
                .AppendPathSegment(PublicKeyEndpoints.Card)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<PublicKeyResponse>();
        }
    }
}
