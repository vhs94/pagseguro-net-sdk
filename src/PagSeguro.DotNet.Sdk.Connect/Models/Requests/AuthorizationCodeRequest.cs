using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Requests
{
    /// <summary>
    /// Dados enviados para trocar o código de autorização (authorization_code) por um access_token.
    /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">ler documentação</see>
    /// </summary>
    public class AuthorizationCodeRequest : AuthorizationRequestBase
    {
        internal override string GrantType => ApiGrants.AuthorizationCode;
        /// <summary>
        /// Código de autorização obtido após o consentimento do usuário.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// URL de redirecionamento, que deve ser idêntica à informada na
        /// solicitação de autorização.
        /// </summary>
        public string? RedirectUri { get; set; }
        /// <summary>
        /// Permissões solicitadas para o token.
        /// </summary>
        public ApiScopes? Scope { get; set; }
    }
}
