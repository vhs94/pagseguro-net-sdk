using Flurl;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Providers
{
    /// <inheritdoc cref="IProvider" />
    public abstract class BaseProvider(PagSeguroSettings settings, IFlurlClient flurlClient) : IProvider
    {
        /// <inheritdoc />
        public PagSeguroSettings Settings { get; set; } = settings;
        /// <inheritdoc />
        public IFlurlClient FlurlClient { get; } = flurlClient;
        /// <inheritdoc />
        public Url BaseUrl => Settings.Environment == PagSeguroEnvironment.Sandbox
            ? CommonEndpoints.SandboxBaseUrl
            : CommonEndpoints.ProductionBaseUrl;
        /// <inheritdoc />
        public IFlurlRequest Request() => FlurlClient.Request(BaseUrl);

        /// <inheritdoc />
        public void EnsureAccessToken()
        {
            if (string.IsNullOrEmpty(Settings.AccessToken))
            {
                throw new ClientNotConnectedException();
            }
        }

        /// <inheritdoc />
        public void EnsureChallenge()
        {
            if (string.IsNullOrEmpty(Settings.Challenge))
            {
                throw new ClientNotConnectedWithChallengeException();
            }
        }

        /// <inheritdoc />
        public void EnsureClientApplication()
        {
            if (string.IsNullOrEmpty(Settings.ClientId) ||
                string.IsNullOrEmpty(Settings.ClientSecret))
            {
                throw new MissingClientApplicationException();
            }
        }

        /// <inheritdoc />
        public void EnsurePrivateKey()
        {
            if (string.IsNullOrEmpty(Settings.PrivateKey))
            {
                throw new PrivateKeyNotFoundException();
            }
        }
    }
}
