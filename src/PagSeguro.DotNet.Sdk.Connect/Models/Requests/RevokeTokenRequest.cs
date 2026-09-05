using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Requests
{
    /// <summary>
    /// Dados enviados para revogar um token, encerrando o acesso à conta do usuário.
    /// <see href="https://developer.pagbank.com.br/reference/revogar-access-token">ler documentação</see>
    /// </summary>
    public class RevokeTokenRequest
    {
        /// <summary>
        /// Token a ser revogado.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Tipo do token informado em <see cref="Token"/>.
        /// </summary>
        public TokenTypeHint TokenTypeHint { get; set; }
    }
}
