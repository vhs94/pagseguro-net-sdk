using Flurl;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Providers
{
    /// <summary>
    /// Base dos providers de Assinaturas. A API de Assinaturas roda em um host
    /// próprio, então a URL base do provider é sobrescrita aqui.
    /// </summary>
    public abstract class SubscriptionBaseProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient)
    {
        /// <inheritdoc />
        public override Url BaseUrl => Settings.Environment == PagSeguroEnvironment.Sandbox
            ? SubscriptionEndpoints.SandboxBaseUrl
            : SubscriptionEndpoints.ProductionBaseUrl;

        /// <summary>
        /// Cria uma requisição autenticada com uma chave de idempotência nova.
        /// </summary>
        protected IFlurlRequest IdempotentRequest()
            => Request()
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(SubscriptionHeaders.IdempotencyKey, Guid.NewGuid().ToString("N"));

        /// <summary>
        /// Cria uma requisição autenticada, sem chave de idempotência.
        /// </summary>
        protected IFlurlRequest AuthorizedRequest()
            => Request().WithOAuthBearerToken(Settings.Token);
    }
}
