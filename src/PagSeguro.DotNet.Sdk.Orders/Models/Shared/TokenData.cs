using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados da tokenização de bandeira (network token) usados na autorização.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-token-bandeira">ler documentação</see>
    /// </summary>
    public class TokenData
    {
        /// <summary>
        /// Identificador do Token Requestor. 11 caracteres.
        /// </summary>
        [JsonPropertyName("requestor_id")]
        public string? RequestorId { get; set; }
        /// <summary>
        /// Carteira digital de origem do token.
        /// Valores possíveis: APPLE_PAY, GOOGLE_PAY, SAMSUNG_PAY e
        /// MERCHANT_TOKENIZATION_PROGRAM.
        /// </summary>
        public string? Wallet { get; set; }
        /// <summary>
        /// Criptograma gerado pela bandeira. 40 caracteres.
        /// </summary>
        public string? Cryptogram { get; set; }
        /// <summary>
        /// Identificador do domínio de origem da transação. 150 caracteres.
        /// </summary>
        [JsonPropertyName("ecommerce_domain")]
        public string? EcommerceDomain { get; set; }
        /// <summary>
        /// Nível de confiança do token.
        /// </summary>
        [JsonPropertyName("assurance_level")]
        public int AssuranceLevel { get; set; }
    }
}
