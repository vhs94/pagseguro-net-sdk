using Flurl;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Cards;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Cards
{
    /// <inheritdoc cref="IAuthenticationSessionProvider" />
    public class AuthenticationSessionProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IAuthenticationSessionProvider
    {
        /// <summary>
        /// A sessão 3DS não fica na API principal: ela é servida pelo host do
        /// SDK de front-end.
        /// </summary>
        protected override Url BaseUrl => Settings.Environment == PagSeguroEnvironment.Sandbox
            ? OrderEndpoint.SandboxSdkBaseUrl
            : OrderEndpoint.ProductionSdkBaseUrl;

        /// <inheritdoc />
        public async Task<AuthenticationSessionResponse> CreateAsync()
        {
            return await Request()
                .AppendPathSegment(OrderEndpoint.AuthenticationSessions)
                .WithOAuthBearerToken(Settings.Token)
                .PostAsync()
                .ReceiveJson<AuthenticationSessionResponse>();
        }
    }
}
