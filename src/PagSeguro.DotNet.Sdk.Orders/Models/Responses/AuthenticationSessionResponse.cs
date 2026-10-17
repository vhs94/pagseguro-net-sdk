using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Sessão usada pelo SDK de front-end para autenticar o portador em 3DS.
    /// <see href="https://developer.pagbank.com.br/reference/criar-sessao-autenticacao-3ds">ler documentação</see>
    /// </summary>
    public class AuthenticationSessionResponse
    {
        /// <summary>
        /// Token criptografado da sessão. Vale por 30 minutos e deve ser
        /// repassado ao SDK de front-end.
        /// </summary>
        public string? Session { get; set; }

        /// <summary>
        /// Momento da expiração da sessão, em milissegundos desde a época Unix.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public long ExpiresAt { get; set; }
    }
}
