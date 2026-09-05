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

        /// <summary>
        /// Renova um access_token expirado a partir do refresh_token.
        /// Cada renovação gera um novo refresh_token e invalida o anterior.
        /// Corresponde a POST /oauth2/refresh.
        /// <see href="https://developer.pagbank.com.br/reference/renovar-access-token">ler documentação</see>
        /// </summary>
        /// <param name="refreshTokenRequest">Refresh token recebido na emissão anterior.</param>
        /// <returns>O novo access_token emitido, com um novo refresh_token.</returns>
        /// <remarks>
        /// <para><strong>Atenção:</strong> requer o ClientId e o ClientSecret configurados
        /// com <c>ConfigureClientApplication()</c>.</para>
        /// </remarks>
        Task<AuthorizationCodeResponse> RefreshAccessTokenAsync(RefreshTokenRequest refreshTokenRequest);

        /// <summary>
        /// Revoga um token, encerrando o acesso à conta do usuário.
        /// Revogar o refresh_token invalida também o access_token associado.
        /// Corresponde a POST /oauth2/revoke.
        /// <see href="https://developer.pagbank.com.br/reference/revogar-access-token">ler documentação</see>
        /// </summary>
        /// <param name="revokeTokenRequest">Token a ser revogado e o seu tipo.</param>
        /// <remarks>
        /// <para><strong>Atenção:</strong> requer o ClientId e o ClientSecret configurados
        /// com <c>ConfigureClientApplication()</c>.</para>
        /// </remarks>
        Task RevokeTokenAsync(RevokeTokenRequest revokeTokenRequest);
    }
}
