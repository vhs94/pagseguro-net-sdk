using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Chave pública usada para criptografar os dados de cartão nas cobranças
    /// recorrentes.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-chave-publica-pagamento-recorrente">ler documentação</see>
    /// </summary>
    public class SubscriptionPublicKeyResponse
    {
        /// <summary>Chave pública no formato RSA.</summary>
        [JsonPropertyName("public_key")]
        public string? PublicKey { get; set; }

        /// <summary>Data e horário de criação da chave.</summary>
        [JsonPropertyName("created_at")]
        public DateTime? CreatedDate { get; set; }
    }
}
