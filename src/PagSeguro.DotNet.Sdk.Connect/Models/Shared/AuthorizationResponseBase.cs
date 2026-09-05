using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Shared
{
    /// <summary>
    /// Dados comuns das respostas de emissão de access_token via OAuth2.
    /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">ler documentação</see>
    /// </summary>
    public abstract class AuthorizationResponseBase
    {
        /// <summary>
        /// Token de acesso utilizado para autenticar as demais chamadas à API.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        /// <summary>
        /// Tipo do token. Sempre bearer.
        /// </summary>
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        /// <summary>
        /// Tempo de validade do access_token, em segundos.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        /// <summary>
        /// Permissões concedidas ao token.
        /// </summary>
        public string? Scope { get; set; }
    }
}
