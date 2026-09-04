using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.PublicKey.Models.Responses
{
    /// <summary>
    /// Chave pública utilizada para criptografar dados sensíveis, como os dados do cartão.
    /// <see href="https://developer.pagbank.com.br/reference/criar-chave-publica">ler documentação</see>
    /// </summary>
    public class PublicKeyResponse
    {
        /// <summary>
        /// Chave pública no formato RSA.
        /// </summary>
        [JsonPropertyName("public_key")]
        public string? PublicKey { get; set; }
        /// <summary>
        /// Data e horário de criação da chave, em milissegundos.
        /// </summary>
        [JsonPropertyName("created_at")]
        public long CreateTimestamp { get; set; }
    }
}
