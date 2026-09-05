using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Responses
{
    /// <summary>
    /// Token de autenticação emitido para a conta criada.
    /// <see href="https://developer.pagbank.com.br/reference/criar-conta">ler documentação</see>
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Tipo do token. Sempre Bearer.
        /// </summary>
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        /// <summary>
        /// Token de acesso e autenticação. Máximo de 100 caracteres.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        /// <summary>
        /// Tempo de validade do access_token, em segundos.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }
        /// <summary>
        /// Token utilizado para atualizar o access_token. Máximo de 100 caracteres.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        /// <summary>
        /// Permissões que foram concedidas ao token.
        /// </summary>
        public string? Scope { get; set; }
    }
}
