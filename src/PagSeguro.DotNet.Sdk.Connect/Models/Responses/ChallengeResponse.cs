using PagSeguro.DotNet.Sdk.Connect.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Responses
{
    /// <summary>
    /// Access token emitido pelo fluxo de desafio (grant_type challenge),
    /// utilizado na emissão do certificado digital.
    /// <see href="https://developer.pagbank.com.br/reference/solicitar-autorizacao-via-connect-authorization">ler documentação</see>
    /// </summary>
    public class ChallengeResponse : AuthorizationResponseBase
    {
        /// <summary>
        /// Desafio criptografado devolvido pelo PagBank.
        /// </summary>
        public string? Challenge { get; set; }
        /// <summary>
        /// Desafio já decriptado com a chave privada da aplicação.
        /// </summary>
        public string? DecryptedChallenge { get; set; }
    }
}
