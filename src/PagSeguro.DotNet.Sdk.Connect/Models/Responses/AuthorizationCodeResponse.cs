using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Connect.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Responses
{
    /// <summary>
    /// Access token emitido a partir de um código de autorização.
    /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">ler documentação</see>
    /// </summary>
    public class AuthorizationCodeResponse : AuthorizationResponseBase
    {
        /// <summary>
        /// Token utilizado para renovar o access_token quando ele expirar.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        /// <summary>
        /// Identificador da conta do usuário que concedeu a autorização.
        /// </summary>
        [JsonPropertyName("account_id")]
        public string? AccountId { get; set; }
    }
}
