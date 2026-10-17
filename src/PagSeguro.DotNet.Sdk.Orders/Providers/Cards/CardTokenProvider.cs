using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Cards;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Cards
{
    /// <inheritdoc cref="ICardTokenProvider" />
    public class CardTokenProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        ICardTokenProvider
    {
        /// <inheritdoc />
        public async Task<CardTokenResponse> CreateAsync(CardTokenRequest cardTokenRequest)
        {
            return await Request()
                .AppendPathSegment(OrderEndpoint.CardTokens)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(cardTokenRequest)
                .ReceiveJson<CardTokenResponse>();
        }
    }
}
