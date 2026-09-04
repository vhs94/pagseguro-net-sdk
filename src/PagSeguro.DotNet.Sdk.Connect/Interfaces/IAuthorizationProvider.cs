using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Connect.Interfaces
{
    /// <summary>
    /// Emissão de access_token pelo Connect (OAuth2).
    /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">ler documentação</see>
    /// </summary>
    public interface IAuthorizationProvider : IProvider
    {
        /// <summary>
        /// Troca o código de autorização (authorization_code) obtido após o
        /// consentimento do usuário por um access_token.
        /// Corresponde a POST /oauth2/token.
        /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">ler documentação</see>
        /// </summary>
        Task<AuthorizationCodeResponse> CreateAccessTokenByCodeAsync(
            AuthorizationCodeRequest authorizationCodeRequest);
        /// <summary>
        /// Emite um access_token pelo fluxo de desafio (grant_type challenge),
        /// usado na emissão do certificado digital. O desafio retornado é
        /// decriptado com a chave privada da aplicação.
        /// <see href="https://developer.pagbank.com.br/reference/solicitar-autorizacao-via-connect-authorization">ler documentação</see>
        /// </summary>
        Task<ChallengeResponse> CreateAccessTokenByChallengeAsync();
    }
}
