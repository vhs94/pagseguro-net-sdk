using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Requests
{
    /// <summary>
    /// Dados enviados para renovar um access_token expirado a partir do refresh_token.
    /// <see href="https://developer.pagbank.com.br/reference/renovar-access-token">ler documentação</see>
    /// </summary>
    public class RefreshTokenRequest : AuthorizationRequestBase
    {
        internal override string GrantType => ApiGrants.RefreshToken;

        /// <summary>
        /// Refresh token recebido na emissão anterior do access_token.
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}
