using Flurl;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Providers
{
    /// <summary>
    /// Base de todos os providers do SDK.
    /// </summary>
    /// <remarks>
    /// Os membros abaixo são <c>protected internal</c> de propósito: os providers
    /// que herdam desta classe precisam deles, mas quem consome o pacote não deve
    /// enxergar as credenciais, o cliente HTTP nem a URL base.
    /// </remarks>
    public abstract class BaseProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
    {
        /// <summary>
        /// Configurações da integração, incluindo as credenciais.
        /// Somente leitura: a instância é compartilhada com o cliente, que atualiza
        /// o access_token após a autenticação.
        /// </summary>
        protected internal PagSeguroSettings Settings { get; } = settings;

        /// <summary>Cliente HTTP usado pelo provider.</summary>
        protected internal IFlurlClient FlurlClient { get; } = flurlClient;

        /// <summary>
        /// URL base da API, definida pelo ambiente configurado.
        /// Sobrescrita pelos providers que rodam em outro host, como o de Assinaturas.
        /// </summary>
        protected internal virtual Url BaseUrl => Settings.Environment == PagSeguroEnvironment.Sandbox
            ? CommonEndpoints.SandboxBaseUrl
            : CommonEndpoints.ProductionBaseUrl;

        /// <summary>Cria uma requisição HTTP apontada para a URL base do ambiente.</summary>
        protected internal IFlurlRequest Request() => FlurlClient.Request(BaseUrl);

        /// <summary>
        /// Garante que um access_token esteja configurado.
        /// </summary>
        /// <exception cref="ClientNotConnectedException">Quando não há access_token.</exception>
        protected internal void EnsureAccessToken()
        {
            if (string.IsNullOrEmpty(Settings.AccessToken))
            {
                throw new ClientNotConnectedException();
            }
        }

        /// <summary>
        /// Garante que um desafio esteja configurado.
        /// </summary>
        /// <exception cref="ClientNotConnectedWithChallengeException">Quando não há desafio.</exception>
        protected internal void EnsureChallenge()
        {
            if (string.IsNullOrEmpty(Settings.Challenge))
            {
                throw new ClientNotConnectedWithChallengeException();
            }
        }

        /// <summary>
        /// Garante que o clientId e o clientSecret estejam configurados.
        /// </summary>
        /// <exception cref="MissingClientApplicationException">Quando faltam as credenciais da aplicação.</exception>
        protected internal void EnsureClientApplication()
        {
            if (string.IsNullOrEmpty(Settings.ClientId) ||
                string.IsNullOrEmpty(Settings.ClientSecret))
            {
                throw new MissingClientApplicationException();
            }
        }

        /// <summary>
        /// Garante que a chave privada esteja configurada.
        /// </summary>
        /// <exception cref="PrivateKeyNotFoundException">Quando não há chave privada.</exception>
        protected internal void EnsurePrivateKey()
        {
            if (string.IsNullOrEmpty(Settings.PrivateKey))
            {
                throw new PrivateKeyNotFoundException();
            }
        }
    }
}
