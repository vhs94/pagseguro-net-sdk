using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Connect.Interfaces
{
    public interface IAuthorizationProvider
    {
        /// <summary>
        /// This endpoint allows you to exchange the authorization code (authorization_code) by one access_token when the user grants permission
        /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">Read the docs</see>
        /// </summary>
        Task<AuthorizationCodeResponse> CreateAccessTokenByCodeAsync(AuthorizationCodeRequest authorizationCodeRequest);

        /// <summary>
        /// This endpoint allows you to exchange the authorization code (challenge) by one access_token when the user grants permission
        /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">Read the docs</see>
        /// </summary>
        Task<ChallengeResponse> CreateAccessTokenByChallengeAsync();
    }
}
