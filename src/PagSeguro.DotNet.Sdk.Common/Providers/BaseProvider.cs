using Flurl;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Providers
{
    public abstract class BaseProvider(PagSeguroSettings settings) : IProvider
    {
        public PagSeguroSettings Settings { get; set; } = settings;
        public Url BaseUrl => Settings.Environment == PagSeguroEnvironment.Sandbox
            ? CommonEndpoints.SandboxBaseUrl
            : CommonEndpoints.ProductionBaseUrl;

        public void EnsureAccessToken()
        {
            if (string.IsNullOrEmpty(Settings.AccessToken))
            {
                throw new ClientNotConnectedException();
            }
        }

        public void EnsureChallenge()
        {
            if (string.IsNullOrEmpty(Settings.Challenge))
            {
                throw new ClientNotConnectedWithChallengeException();
            }
        }

        public void EnsureClientApplication()
        {
            if (string.IsNullOrEmpty(Settings.ClientId) ||
                string.IsNullOrEmpty(Settings.ClientSecret))
            {
                throw new MissingClientApplicationException();
            }
        }

        public void EnsurePrivateKey()
        {
            if (string.IsNullOrEmpty(Settings.PrivateKey))
            {
                throw new PrivateKeyNotFoundException();
            }
        }
    }
}
