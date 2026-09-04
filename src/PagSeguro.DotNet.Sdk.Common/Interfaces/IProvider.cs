using Flurl;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Interfaces
{
    /// <summary>
    /// Contrato comum a todos os providers do SDK, expondo as configurações
    /// e o cliente HTTP utilizados nas chamadas à API do PagBank.
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Configurações da integração em uso.
        /// </summary>
        public PagSeguroSettings Settings { get; set; }
        /// <summary>
        /// URL base da API, definida pelo ambiente configurado (sandbox ou produção).
        /// </summary>
        public Url BaseUrl { get; }
        /// <summary>
        /// Cliente HTTP utilizado pelo provider.
        /// </summary>
        public IFlurlClient FlurlClient { get; }
        /// <summary>
        /// Cria uma requisição HTTP já apontada para a URL base do ambiente.
        /// </summary>
        public IFlurlRequest Request() => FlurlClient.Request(BaseUrl);
        /// <summary>
        /// Garante que um access_token esteja configurado.
        /// Lança uma exceção de validação caso contrário.
        /// </summary>
        void EnsureAccessToken();
        /// <summary>
        /// Garante que um desafio (challenge) esteja configurado.
        /// Lança uma exceção de validação caso contrário.
        /// </summary>
        void EnsureChallenge();
        /// <summary>
        /// Garante que o clientId e o clientSecret da aplicação
        /// estejam configurados. Lança uma exceção de validação caso contrário.
        /// </summary>
        void EnsureClientApplication();
        /// <summary>
        /// Garante que a chave privada esteja configurada.
        /// Lança uma exceção de validação caso contrário.
        /// </summary>
        void EnsurePrivateKey();
    }
}
