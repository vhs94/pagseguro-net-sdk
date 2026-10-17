using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Responses
{
    /// <summary>
    /// Confirmação de que o SMS de autorização foi enviado ao vendedor.
    /// <see href="https://developer.pagbank.com.br/reference/solicitar-autorizacao-via-sms">ler documentação</see>
    /// </summary>
    public class SmsAuthorizationResponse
    {
        /// <summary>
        /// Identificador da autorização pendente. Deve ser informado junto com o
        /// código recebido por SMS na emissão do access_token.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>Telefone mascarado para o qual o SMS foi enviado.</summary>
        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>Segundos de espera até ser possível pedir um novo SMS.</summary>
        [JsonPropertyName("retry_after_seconds")]
        public int RetryAfterSeconds { get; set; }
    }
}
