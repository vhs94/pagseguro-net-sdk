using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Dados do cartão enviados na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public class CardRequest : CardBase
    {
        /// <summary>
        /// Número do cartão de crédito ou débito. De 14 a 19 caracteres.
        /// </summary>
        public string? Number { get; set; }
        /// <summary>
        /// Código de segurança do cartão (CVV). De 3 a 4 caracteres.
        /// </summary>
        [JsonPropertyName("security_code")]
        public int SecurityCode { get; set; }
    }
}
